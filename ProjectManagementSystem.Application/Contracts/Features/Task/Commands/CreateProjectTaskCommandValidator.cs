using FluentValidation;
using ProjectManagementSystem.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Commands
{
    public class CreateProjectTaskCommandValidator : AbstractValidator<CreateProjectTaskCommand>
    {
        private readonly IProjectRepository _projectRepository;
        public CreateProjectTaskCommandValidator(IProjectRepository projectRepository)
        {


            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Deadline).GreaterThan(DateTime.Now);
            RuleFor(x => x.ProjectId).GreaterThan(0).MustAsync(ProjectMustExist).WithMessage("{PropertyName} does not exist");
            RuleFor(x => x.UserId).GreaterThan(0);

            _projectRepository = projectRepository;
        }

        private async Task<bool> ProjectMustExist(int projectId, CancellationToken token)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            return project != null;
        }
    }

}
