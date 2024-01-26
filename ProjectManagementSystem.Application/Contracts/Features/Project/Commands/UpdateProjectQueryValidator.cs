using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Project.Commands
{
    public class UpdateProjectQueryValidator : AbstractValidator<UpdateProjectQuery>
    {
        public UpdateProjectQueryValidator()
        {
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("ProjectId must be greater than 0.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        }
    }

}
