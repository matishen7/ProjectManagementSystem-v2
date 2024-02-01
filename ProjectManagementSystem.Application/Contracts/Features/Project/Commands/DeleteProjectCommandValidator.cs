using FluentValidation;
using ProjectManagementSystem.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Project.Commands
{
    public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectCommandValidator(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            RuleFor(x => x.ProjectId).NotEmpty().NotNull().MustAsync(ProjectExists).WithMessage("Invalid project id specified."); ;
        }

        private async Task<bool> ProjectExists(int projectId, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            return project != null;
        }
    }

}
