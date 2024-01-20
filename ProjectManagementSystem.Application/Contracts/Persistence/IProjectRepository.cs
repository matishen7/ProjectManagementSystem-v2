using ProjectManagementSystem.Application.Contracts.Features.Project.Queries;
using ProjectManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Persistence
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        public Task CreateProjectAsync(Project project);
    }
}
