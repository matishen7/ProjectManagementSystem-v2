using AutoMapper;
using FluentValidation;
using ProjectManagementSystem.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Commands
{
    public class UpdateProjectTaskCommandValidator : AbstractValidator<UpdateProjectTaskCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IUserRepository _userRepository;

        public UpdateProjectTaskCommandValidator(
            IProjectTaskRepository projectTaskRepository,
            IUserRepository userRepository,
            IProjectRepository projectRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        
            RuleFor(x => x.TaskId).GreaterThan(0).MustAsync(ProjectTaskMustExist).WithMessage("{PropertyName} does not exist"); 
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Deadline).GreaterThan(DateTime.Now);
            RuleFor(x => x.ProjectId).GreaterThan(0).MustAsync(ProjectMustExist).WithMessage("{PropertyName} does not exist");
            RuleFor(x => x.UserId).GreaterThan(0).MustAsync(UserMustExist).WithMessage("{PropertyName} does not exist"); 
        }
        private async Task<bool> ProjectMustExist(int projectId, CancellationToken token)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            return project != null;
        }

        private async Task<bool> ProjectTaskMustExist(int projectTaskId, CancellationToken token)
        {
            var projectTask = await _projectTaskRepository.GetByIdAsync(projectTaskId);
            return projectTask != null;
        }

        private async Task<bool> UserMustExist(int? userId, CancellationToken token)
        {
            var user = await _userRepository.GetByIdAsync(userId.Value);
            return user != null;
        }
    }

}
