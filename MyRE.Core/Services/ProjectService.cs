﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MyRE.Core.Models.Domain;
using MyRE.Core.Repositories;

namespace MyRE.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Task<IEnumerable<Project>> GetUserProjectsAsync(string userId) => _projectRepository.GetUserProjectsAsync(userId);
    }
}