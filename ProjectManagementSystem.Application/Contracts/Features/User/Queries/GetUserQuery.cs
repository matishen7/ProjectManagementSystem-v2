using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.User.Queries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }
    }
}
