using AutoMapper;
using MediatR;
using ProjectManagementSystem.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetProjectTaskQueryHandler : IRequestHandler<GetProjectTaskQuery, ProjectTaskDto>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public GetProjectTaskQueryHandler(IProjectTaskRepository projectTaskRepository, IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public async Task<ProjectTaskDto> Handle(GetProjectTaskQuery request, CancellationToken cancellationToken)
        {
            var task = await _projectTaskRepository.GetByIdAsync(request.TaskId);
            return _mapper.Map<ProjectTaskDto>(task);
        }
    }
}
