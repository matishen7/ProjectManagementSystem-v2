using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetProjectTaskQuery : IRequest<ProjectTaskDto>
    {
        public int TaskId { get; set; }
    }
}
