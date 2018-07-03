using Abp.AutoMapper;
using Abp.Domain.Repositories;
using AutoMapper;
using ProjectManagementSystem.Projects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Projects
{
    public class ProjectAppService : ProjectManagementSystemAppServiceBase, IProjectAppService
    {

        private readonly IRepository<Project> _projectRepository;

        public ProjectAppService(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public ProjectSearchOutputDto SearchProjects(ProjectSearchInputDto input)
        {
            var query = _projectRepository.GetAll();

            if (input.TeamLeaderId.HasValue)
            {
                query = query.Where(t => t.TeamLeaderId == input.TeamLeaderId);
            }

            if (input.IsFinished.HasValue)
            {
                query = query.Where(t => t.IsFinished == input.IsFinished);
            }

            return new ProjectSearchOutputDto
            {
                Projects = Mapper.Map<List<ProjectDto>>(query.ToList())
            };
        }

        public int CreateProject(CreateProjectDto input)
        {
            Logger.Info("Creating a project for input: " + input);

            var project = new Project
            {
                Name = input.Name,
                Description = input.Description,
                StartTime = input.StartTime,
                DeliverTime = input.DeliverTime
            };

            return _projectRepository.InsertAndGetId(project);
        }

        public void DeleteProject(int projectId)
        {
            var project = _projectRepository.Get(projectId);
            if (project != null)
            {
                _projectRepository.Delete(project);
            }
        }

        //not realized
        public IList<ProjectDto> GetAllProjects()
        {
            var query = _projectRepository.GetAll();

            throw new NotImplementedException();
        }

        public ProjectDto GetProjectById(int projectId)
        {
            var project = _projectRepository.Get(projectId);

            return project.MapTo<ProjectDto>();
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);

            return project.MapTo<ProjectDto>();
        }

        public void UpdateProject(UpdateProjectDto input)
        {
            Logger.Info("Updating a project for input: " + input);

            var project = _projectRepository.Get(input.Id);

            project.IsFinished = input.IsFinished;
            
            if (input.TeamLeaderId.HasValue)
            {
                project.TeamLeaderId = input.TeamLeaderId;
            }
        }
    }
}
