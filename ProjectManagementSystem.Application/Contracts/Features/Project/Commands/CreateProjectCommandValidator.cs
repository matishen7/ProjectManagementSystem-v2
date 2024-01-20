using FluentValidation;
using ProjectManagementSystem.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Project.Commands
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateProjectCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).When(x => x.EndDate.HasValue);
            RuleFor(x => x.ProjectManagerId).NotEmpty().GreaterThan(0).MustAsync(ExistInUserRepository).WithMessage("Invalid project manager id specified.");
        }

        private async Task<bool> ExistInUserRepository(int projectManagerId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(projectManagerId);

            return user != null;
        }
    }
}
