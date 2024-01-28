using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.Task.Queries
{
    public class GetAllProjectTasksForUserQuery : IRequest<List<ProjectTaskDto>>
    {
        public int UserId { get; set; }
    }

}
