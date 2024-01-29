using ProjectManagementSystem.Application.Contracts.Features.Task.Queries;
using ProjectManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Persistence
{
    public interface IProjectTaskRepository : IGenericRepository<ProjectTask>
    {
        public Task<ProjectTask> GetProjectTaskByIdAsync(int Id);
        public Task<List<ProjectTask>> GetTasksForProjectAsync(int projectId);
        public Task<List<ProjectTask>> GetTasksForAssignedUserAsync(int userId);
        public Task<List<ProjectTask>> GetTasksForAssignedUserAndProjectAsync(int userId, int projectId);
    }
}
