﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRE.Core.Extensions;
using MyRE.Core.Models.Domain;
using MyRE.Core.Models.Web;
using MyRE.Core.Services;
using MyRE.Web.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyRE.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Projects")]
    [Authorize]
    public class ProjectsController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IProjectService _project;
        private readonly IUserService _user;
        private readonly ISmartAppService _smartApp;

        private readonly IProjectMappingService _projectMappingService;

        public ProjectsController(IProjectService project, IUserService user, IAuthorizationService authorizationService, IProjectMappingService projectMappingService, ISmartAppService smartApp)
        {
            _project = project;
            _user = user;
            _authorizationService = authorizationService;
            _projectMappingService = projectMappingService;
            _smartApp = smartApp;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<Project>), 200)]
        public async Task<IEnumerable<Project>> ListProjectsAsync()
        {
            var currentUser = await _user.GetAuthenticatedUserFromContextAsync(HttpContext);
            var projects = await _project.GetUserProjectsAsync(currentUser.UserId);

            return projects.Select(_projectMappingService.ToDomain);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(Project), 201)]
        public async Task<IActionResult> CreateNewProject([FromBody]Project newProject)
        {
            var createdProject = await _project.CreateAsync(newProject.Name, newProject.Description, newProject.InstanceId);

            var instanceProjectUpsertResult = await _smartApp.UpsertProjectAsync(createdProject);

            return Created(GetUriOfResource($"/api/Projects/{createdProject.ProjectId}"), _projectMappingService.ToDomain(createdProject));
        }

        [HttpGet("{projectId:Guid}")]
        [ProducesResponseType(typeof(Project), 200)]
        public async Task<IActionResult> GetProjectAsync(Guid projectId)
        {
            return await RetrieveAuthenticatedResource(
                _authorizationService, 
                () => _project.GetByIdAsync(projectId),
                project => Task.FromResult(Ok(_projectMappingService.ToDomain(project))));
        }

        [HttpPut("{projectId:Guid}")]
        public async Task<IActionResult> UpdateProjectAsync([FromRoute] Guid projectId, [FromBody] JToken rawBody)
        {
            var body = rawBody.ToObject<Project>();

            var localPersistResult = await _project.UpdateAsync(_projectMappingService.ToData((Project) body));

            var instanceProjectUpsertResult = await _smartApp.UpsertProjectAsync(localPersistResult);

            return Ok(_projectMappingService.ToDomain(localPersistResult));
        }

        [HttpDelete("{projectId:Guid}")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> DeleteProject(Guid projectId)
        {
            return await DeleteAuthenticatedResource(_authorizationService, () => _project.GetByIdAsync(projectId),
                project => _project.DeleteAsync(projectId));
        }
    }
}