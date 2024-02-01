using FluentValidation;
using ProjectManagementSystem.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetAllProjectTasksForUserQueryValidator : AbstractValidator<GetAllProjectTasksForUserQuery>
    {
        private IUserRepository _userRepository;

        public GetAllProjectTasksForUserQueryValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0.")
                .MustAsync(UserMustExist).WithMessage("{PropertyName} does not exist"); ;
        }
        private async Task<bool> UserMustExist(int userId, CancellationToken token)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null;
        }
    }

}
