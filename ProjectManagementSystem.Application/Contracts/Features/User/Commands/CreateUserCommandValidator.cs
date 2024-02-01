using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManagementSystem.Application.Contracts.Features.User.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(command => command.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(command => command.Email).NotEmpty();//.EmailAddress().WithMessage("Invalid email address");
        }
    }

}
