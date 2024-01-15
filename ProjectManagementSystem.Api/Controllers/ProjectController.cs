﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Contracts.Features.Project.Queries;

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
    }
}