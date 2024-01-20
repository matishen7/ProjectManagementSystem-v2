using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Contracts.Features.User.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
    }
}
