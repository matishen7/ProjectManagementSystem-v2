using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.Contracts.Features.Task.Queries;
using ProjectManagementSystem.Application.Contracts.Persistence;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Persistance.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Persistance.Repositories
{
    public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(ProjectManagementDbContext context) : base(context)
        {
        }

        public Task<ProjectTask> GetProjectTaskByIdAsync(int Id)
        {
            return _context.ProjectTasks
                .FirstOrDefaultAsync(x=>x.Id == Id);

        }

        public Task<List<ProjectTask>> GetTasksForAssignedUserAndProjectAsync(int userId, int projectId)
        {
            return _context.ProjectTasks
                .Where(q => q.UserId == userId && q.ProjectId == projectId)
                .ToListAsync();
        }

        public Task<List<ProjectTask>> GetTasksForAssignedUserAsync(int userId)
        {
            return _context.ProjectTasks
                .Where(q => q.UserId == userId)
                .ToListAsync();
        }

        public Task<List<ProjectTask>> GetTasksForProjectAsync(int projectId)
        {
            return _context.ProjectTasks
               .Where(q => q.ProjectId == projectId)
               .ToListAsync();
        }
    }
}
