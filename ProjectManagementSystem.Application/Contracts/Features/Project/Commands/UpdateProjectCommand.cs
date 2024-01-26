using MediatR;
using ProjectManagementSystem.Application.Contracts.Features.Project.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Project.Commands
{
    public class UpdateProjectCommand : IRequest<ProjectDto>
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

}
