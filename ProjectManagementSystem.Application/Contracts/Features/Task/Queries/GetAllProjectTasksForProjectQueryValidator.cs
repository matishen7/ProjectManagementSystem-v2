using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetAllProjectTasksForProjectQueryValidator : AbstractValidator<GetAllProjectTasksForProjectQuery>
    {
        public GetAllProjectTasksForProjectQueryValidator()
        {
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("ProjectId must be greater than 0.");
        }
    }

}
