﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Contracts.Features.Project.Commands;
using ProjectManagementSystem.Application.Contracts.Features.Project.Queries;
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

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProject(int projectId)
        {
            var project = await _mediator.Send(new GetProjectQuery { ProjectId = projectId });
            return Ok(project);
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
                return StatusCode(500, "An error occurred while processing the request");
            }
        }
    }
}
