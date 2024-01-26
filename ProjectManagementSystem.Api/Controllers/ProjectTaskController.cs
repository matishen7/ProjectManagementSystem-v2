using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Contracts.Features.Task.Queries;
using ProjectManagementSystem.Application.Middleware;

namespace ProjectManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{projectTaskId}")]
        public async Task<ActionResult<ProjectTaskDto>> GetProjectTask(int projectTaskId)
        {
            try
            {
                var query = new GetProjectTaskQuery { ProjectTaskId = projectTaskId };

                var projectTaskDto = await _mediator.Send(query);

                return Ok(projectTaskDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

    }
}
