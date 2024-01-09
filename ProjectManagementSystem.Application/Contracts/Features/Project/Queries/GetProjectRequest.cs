using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Project.Queries
{
    public class GetProjectQuery : IRequest<ProjectDto>
    {
        public int ProjectId { get; set; }
    }
}
