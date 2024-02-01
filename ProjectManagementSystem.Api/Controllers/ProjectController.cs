using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Contracts.Features.Project.Commands;
using ProjectManagementSystem.Application.Contracts.Features.Project.Queries;
using ProjectManagementSystem.Application.Contracts.Features.Task.Queries;
using ProjectManagementSystem.Application.Middleware;

namespace ProjectManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAllProjects()
        {
            var query = new GetAllProjectsQuery();
            var projectsDto = await _mediator.Send(query);
            return Ok(projectsDto);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProject(int projectId)
        {
            try
            {
                var project = await _mediator.Send(new GetProjectQuery { ProjectId = projectId });

                return Ok(project);
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
                return StatusCode(500, $"An error occurred while processing the request {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand createProjectCommand)
        {
            try
            {
                var projectId = await _mediator.Send(createProjectCommand);

                return Ok($"Project created successfully with ID: {projectId}");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDto>> UpdateProject(UpdateProjectCommand request)
        {
            try
            {
                var projectDto = await _mediator.Send(request);
                return Ok(projectDto);
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
                return StatusCode(500, $"An error occurred while processing the request {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject([FromBody] DeleteProjectCommand deleteProjectCommand)
        {
            try
            {
                await _mediator.Send(deleteProjectCommand);

                return Ok("Project successfully deleted.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request {ex.Message}");
            }
        }
    }
}
