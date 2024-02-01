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
    public class UpdateProjectTaskCommandHandler : IRequestHandler<UpdateProjectTaskCommand, Unit>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public UpdateProjectTaskCommandHandler(
            IProjectTaskRepository projectTaskRepository,
            IMapper mapper,
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateProjectTaskCommandValidator(
                _projectTaskRepository,
                _userRepository, 
                _projectRepository).ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingTask = await _projectTaskRepository.GetByIdAsync(request.TaskId);
            if (existingTask == null)
            {
                throw new NotFoundException(nameof(ProjectTask), request.TaskId);
            }

            _mapper.Map(request, existingTask);

            await _projectTaskRepository.UpdateAsync(existingTask);

            return Unit.Value;
        }
    }

}
