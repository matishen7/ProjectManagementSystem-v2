using FluentValidation;
using ProjectManagementSystem.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetAllProjectTasksForProjectQueryValidator : AbstractValidator<GetAllProjectTasksForProjectQuery>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectTasksForProjectQueryValidator(IProjectRepository projectRepository)
        {
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("ProjectId must be greater than 0.")
                .MustAsync(ProjectExists).WithMessage("Invalid project id specified."); 
            _projectRepository = projectRepository;
        }
        private async Task<bool> ProjectExists(int projectId, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            return project != null;
        }
    }

}
