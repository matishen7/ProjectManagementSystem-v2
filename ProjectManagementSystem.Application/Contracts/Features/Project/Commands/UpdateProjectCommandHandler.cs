using AutoMapper;
using FluentValidation;
using MediatR;
using ProjectManagementSystem.Application.Contracts.Features.Project.Queries;
using ProjectManagementSystem.Application.Contracts.Persistence;
using ProjectManagementSystem.Application.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = ProjectManagementSystem.Application.Middleware.ValidationException;

namespace ProjectManagementSystem.Application.Contracts.Features.Project.Commands
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper; 
        private readonly IUserRepository _userRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProjectCommandValidator(_userRepository, _projectRepository);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingProject = await _projectRepository.GetByIdAsync(request.ProjectId);

            if (existingProject == null)
            {
               throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            existingProject.Title = request.Title;
            existingProject.Description = request.Description;
            existingProject.StartDate = request.StartDate;
            existingProject.EndDate = request.EndDate;
            existingProject.ProjectManagerId = request.ProjectManagerId;

            await _projectRepository.UpdateAsync(existingProject);

            return _mapper.Map<ProjectDto>(existingProject);
        }
    }

}
