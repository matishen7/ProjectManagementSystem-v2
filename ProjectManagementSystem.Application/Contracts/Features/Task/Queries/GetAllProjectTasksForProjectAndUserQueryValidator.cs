using FluentValidation;
using ProjectManagementSystem.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetAllProjectTasksForProjectAndUserQueryValidator : AbstractValidator<GetAllProjectTasksForProjectAndUserQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectTasksForProjectAndUserQueryValidator(IUserRepository userRepository, IProjectRepository projectRepository)
        {
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("ProjectId must be greater than 0.")
                .MustAsync(ProjectExists).WithMessage("Invalid project id specified."); ;
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0.")
                .MustAsync(UserMustExist).WithMessage("{PropertyName} does not exist");
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }
        private async Task<bool> UserMustExist(int userId, CancellationToken token)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null;
        }
        private async Task<bool> ProjectExists(int projectId, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            return project != null;
        }
    }

}
