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
    public class GetProjectTaskHandler : IRequestHandler<GetProjectTaskQuery, ProjectTaskDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public GetProjectTaskHandler(IProjectTaskRepository projectTaskRepository, 
            IMapper mapper, 
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<ProjectTaskDto> Handle(GetProjectTaskQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetProjectTaskQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var projectTask = await _projectTaskRepository.GetByIdAsync(request.ProjectTaskId);

            if (projectTask == null)
            {
                throw new NotFoundException(nameof(ProjectTask), request.ProjectTaskId);
            }

            var project = await _projectRepository.GetByIdAsync(projectTask.ProjectId);

            if (project == null)
            {
                throw new NotFoundException(nameof(Project), projectTask.ProjectId);
            }

            

            var projectTaskDto = _mapper.Map<ProjectTaskDto>(projectTask);
            projectTaskDto.ProjectName = project.Title;

            UserEntity user = null;
            if (projectTask.UserId != null)
            {
                user = await _userRepository.GetByIdAsync(projectTask.UserId.Value);

                if (user == null)
                {
                    throw new NotFoundException(nameof(UserEntity), projectTask.UserId.Value);
                }
                projectTaskDto.AssignedUserName = string.Format("{0} {1}", user.FirstName, user.LastName);
            }
            return projectTaskDto;
        }
    }

}
