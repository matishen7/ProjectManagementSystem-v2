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
    public class GetAllProjectTasksForProjectAndUserQueryHandler : IRequestHandler<GetAllProjectTasksForProjectAndUserQuery, List<ProjectTaskDto>>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectTasksForProjectAndUserQueryHandler(
            IProjectTaskRepository projectTaskRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IProjectRepository projectRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectTaskDto>> Handle(GetAllProjectTasksForProjectAndUserQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllProjectTasksForProjectAndUserQueryValidator(_userRepository, _projectRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var projectTasks = await _projectTaskRepository.GetTasksForAssignedUserAndProjectAsync(request.UserId, request.ProjectId);
            var projectTaskDtos = _mapper.Map<List<ProjectTaskDto>>(projectTasks);
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);

            if (project == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(UserEntity), request.UserId);
            }

            projectTaskDtos.ForEach(task => task.AssignedUserName = $"{user.FirstName} {user.LastName}");
            projectTaskDtos.ForEach(task => task.ProjectName = project.Title);
            return projectTaskDtos;
        }
    }

}
