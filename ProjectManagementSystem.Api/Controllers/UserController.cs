using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Contracts.Features.Project.Queries;
using ProjectManagementSystem.Application.Contracts.Features.User.Commands;
using ProjectManagementSystem.Application.Contracts.Features.User.Queries;

namespace ProjectManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _mediator.Send(new GetUserQuery { UserId = userId });
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserCommand command)
        {
            if (userId != command.UserId)
            {
                return BadRequest("Mismatched user ID in the route and body.");
            }

            await _mediator.Send(command);

            return NoContent();
        }

    }


}
