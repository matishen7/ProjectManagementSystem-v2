using AutoMapper;
using MediatR;
using ProjectManagementSystem.Application.Contracts.Features.Project.Commands;
using ProjectManagementSystem.Application.Contracts.Persistence;
using ProjectManagementSystem.Application.Middleware;

namespace ProjectManagementSystem.Application.Contracts.Features.User.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateProjectCommandHandler(
            IProjectRepository projectRepository,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProjectCommandValidator(_userRepository);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var projectEntity = _mapper.Map<Core.Entities.Project>(request);

            await _projectRepository.CreateAsync(projectEntity);

            return projectEntity.Id;
        }
    }
}
