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
    public class GetAllProjectTasksForProjectQueryHandler : IRequestHandler<GetAllProjectTasksForProjectQuery, List<ProjectTaskDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public GetAllProjectTasksForProjectQueryHandler(IProjectTaskRepository projectTaskRepository,
            IMapper mapper, 
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<List<ProjectTaskDto>> Handle(GetAllProjectTasksForProjectQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllProjectTasksForProjectQueryValidator(_projectRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var projectTasks = await _projectTaskRepository.GetTasksForProjectAsync(request.ProjectId);
            var projectTaskDtos = _mapper.Map<List<ProjectTaskDto>>(projectTasks);

            foreach (var projectTask in projectTaskDtos)
            {
                UserEntity user = null;
                if (projectTask.UserId != null && projectTask.UserId != 0)
                {
                    user = await _userRepository.GetByIdAsync(projectTask.UserId.Value);

                    if (user == null)
                    {
                        throw new NotFoundException(nameof(Project), projectTask.ProjectId);
                    }

                    projectTask.AssignedUserName = $"{user?.FirstName} {user?.LastName}";
                }
            }

            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (project != null)
                projectTaskDtos.ForEach(task => task.ProjectName = project.Title);

            return projectTaskDtos;
        }
    }

}
