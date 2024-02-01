using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace ProjectManagementSystem.Application.Contracts.Features.User.Commands
{

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0");

            RuleFor(command => command.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(command => command.LastName).NotEmpty().WithMessage("LastName is required");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("Email is required");
                //.EmailAddress().WithMessage("Invalid email address format");
        }
    }

}
