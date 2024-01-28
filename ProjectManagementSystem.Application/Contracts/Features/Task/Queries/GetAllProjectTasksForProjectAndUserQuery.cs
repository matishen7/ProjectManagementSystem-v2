using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetAllProjectTasksForProjectAndUserQuery : IRequest<List<ProjectTaskDto>>
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }

}
