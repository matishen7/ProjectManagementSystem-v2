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
    public class GetAllProjectTasksQueryHandler : IRequestHandler<GetAllProjectTasksQuery, List<ProjectTaskDto>>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public GetAllProjectTasksQueryHandler(IProjectTaskRepository projectTaskRepository, IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectTaskDto>> Handle(GetAllProjectTasksQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllProjectTasksQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var projectTasks = await _projectTaskRepository.GetTasksForProjectAsync(request.ProjectId);
            var projectTaskDtos = _mapper.Map<List<ProjectTaskDto>>(projectTasks);

            return projectTaskDtos;
        }
    }

}
