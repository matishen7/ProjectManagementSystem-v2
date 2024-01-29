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

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Commands
{
    public class CreateProjectTaskHandler : IRequestHandler<CreateProjectTaskCommand, int>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public CreateProjectTaskHandler(IProjectTaskRepository projectTaskRepository, 
            IMapper mapper,
            IProjectRepository projectRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProjectTaskCommandValidator(_projectRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var projectTaskEntity = _mapper.Map<ProjectTask>(request);

            await _projectTaskRepository.CreateAsync(projectTaskEntity);

            return projectTaskEntity.Id;
        }
    }

}
