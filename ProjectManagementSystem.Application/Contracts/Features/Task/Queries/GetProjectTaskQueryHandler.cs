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
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public GetProjectTaskHandler(IProjectTaskRepository projectTaskRepository, IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public async Task<ProjectTaskDto> Handle(GetProjectTaskQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetProjectTaskQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var projectTask = await _projectTaskRepository.GetProjectTaskByIdAsync(request.ProjectTaskId);

            if (projectTask == null)
            {
                throw new NotFoundException(nameof(ProjectTask), request.ProjectTaskId);
            }

            var projectTaskDto = _mapper.Map<ProjectTaskDto>(projectTask);

            return projectTaskDto;
        }
    }

}
