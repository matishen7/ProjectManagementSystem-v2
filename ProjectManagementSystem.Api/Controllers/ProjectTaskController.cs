using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Contracts.Features.Task.Commands;
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

        [HttpGet("project/{projectId}/tasks")]
        public async Task<IActionResult> GetProjectTasksForProject(int projectId)
        {
            try
            {
                var query = new GetAllProjectTasksForProjectQuery { ProjectId = projectId };
                var projectTaskDtos = await _mediator.Send(query);

                if (projectTaskDtos == null || !projectTaskDtos.Any())
                {
                    return NotFound($"No project tasks found for project with ID {projectId}.");
                }

                return Ok(projectTaskDtos);
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

        [HttpGet("user/{userId}/tasks")]
        public async Task<IActionResult> GetProjectTasksForAssignedUser(int userId)
        {
            try
            {
                var query = new GetAllProjectTasksForUserQuery { UserId = userId };
                var projectTaskDtos = await _mediator.Send(query);

                if (projectTaskDtos == null || !projectTaskDtos.Any())
                {
                    return NotFound($"No project tasks found for user with ID {userId}.");
                }

                return Ok(projectTaskDtos);
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

        [HttpGet("tasks")]
        public async Task<IActionResult> GetProjectTasks(int userId, int projectId)
        {
            try
            {
                var query = new GetAllProjectTasksForProjectAndUserQuery { UserId = userId , ProjectId = projectId};
                var projectTaskDtos = await _mediator.Send(query);

                if (projectTaskDtos == null || !projectTaskDtos.Any())
                {
                    return NotFound($"No project tasks found for user with ID {userId} and project with ID {projectId}");
                }

                return Ok(projectTaskDtos);
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

        [HttpPost]
        public async Task<IActionResult> CreateProjectTask(CreateProjectTaskCommand command)
        {
            try
            {
                var taskId = await _mediator.Send(command);
                return Ok($"Project task is created successfully with ID {taskId}.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request : {ex.Message}");
            }
        }


    }
}
