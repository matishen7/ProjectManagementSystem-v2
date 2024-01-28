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

        public Task<List<ProjectTask>> GetTasksForProjectAsync(int projectId)
        {
            return _context.ProjectTasks
               .Where(q => q.ProjectId == projectId)
               .Include(q => q.Project)
               .ToListAsync();
        }
    }
}
