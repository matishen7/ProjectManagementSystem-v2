using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetProjectTaskQueryValidator : AbstractValidator<GetProjectTaskQuery>
    {
        public GetProjectTaskQueryValidator()
        {
            RuleFor(x => x.ProjectTaskId).GreaterThan(0).WithMessage("ProjectTaskId must be greater than 0.");
        }
    }

}
