using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetAllProjectTasksForProjectAndUserQueryValidator : AbstractValidator<GetAllProjectTasksForProjectAndUserQuery>
    {
        public GetAllProjectTasksForProjectAndUserQueryValidator()
        {
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("ProjectId must be greater than 0.");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0.");
        }
    }

}
