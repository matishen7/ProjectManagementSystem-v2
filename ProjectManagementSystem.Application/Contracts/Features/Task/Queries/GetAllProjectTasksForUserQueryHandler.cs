using AutoMapper;
using MediatR;
using ProjectManagementSystem.Application.Contracts.Persistence;
using ProjectManagementSystem.Application.Middleware;
using ProjectManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetAllProjectTasksForUserQueryHandler : IRequestHandler<GetAllProjectTasksForUserQuery, List<ProjectTaskDto>>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public GetAllProjectTasksForUserQueryHandler(IProjectTaskRepository projectTaskRepository,
            IMapper mapper,
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<List<ProjectTaskDto>> Handle(GetAllProjectTasksForUserQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllProjectTasksForUserQueryValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            var projectTasks = await _projectTaskRepository.GetTasksForAssignedUserAsync(request.UserId);
            var projectTaskDtos = _mapper.Map<List<ProjectTaskDto>>(projectTasks);
            foreach (var projectTask in projectTaskDtos)
            {
                var project = await _projectRepository.GetByIdAsync(projectTask.ProjectId);

                if (project == null)
                {
                    throw new NotFoundException(nameof(Project), projectTask.ProjectId);
                }

                projectTask.ProjectName = project.Title;
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user != null)
                projectTaskDtos.ForEach(task => task.AssignedUserName = $"{user.FirstName} {user.LastName}");

            return projectTaskDtos;
        }
    }

}
