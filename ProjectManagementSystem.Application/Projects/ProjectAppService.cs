using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using AutoMapper;
using ProjectManagementSystem.Authorization;
using ProjectManagementSystem.Authorization.Users;
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
        private readonly IRepository<User, long> _userRepository;

        public ProjectAppService(IRepository<Project> projectRepository, IRepository<User, long> userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public ProjectSearchOutputDto SearchProjects(ProjectSearchInputDto input)
        {
            var query = _projectRepository.GetAll();

            if (input.TeamLeaderId.HasValue)
            {
                query = query.Where(t => t.TeamLeaderId == input.TeamLeaderId);
            }

            if (input.State.HasValue)
            {
                query = query.Where(t => t.State == input.State);
            }

            var list = query.ToList();
            foreach(var project in list)
            {
                if(project.TeamLeaderId.HasValue)
                    project.TeamLeader = _userRepository.Get(ObjectMapper.Map<long>(project.TeamLeaderId));
            }

            return new ProjectSearchOutputDto
            {
                Projects = Mapper.Map<List<ProjectDto>>(list)
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Projects)]
        public int CreateProject(CreateProjectDto input)
        {
            Logger.Info("Creating a project for input: " + input);

            var project = new Project
            {
                Name = input.Name,
                Description = input.Description,
                StartTime = input.StartTime,
                DeliverTime = input.DeliverTime,
                TeamLeaderId = input.TeamLeaderId
            };

            if (input.TeamLeaderId.HasValue)
            {
                var user = _userRepository.Get(ObjectMapper.Map<long>(input.TeamLeaderId));
                project.TeamLeader = user;
            }

            return _projectRepository.InsertAndGetId(project);
        }

        [AbpAuthorize(PermissionNames.Pages_Projects)]
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

        [AbpAuthorize(PermissionNames.Pages_Projects)]
        public void UpdateProject(UpdateProjectDto input)
        {
            Logger.Info("Updating a project for input: " + input);

            var project = _projectRepository.Get(input.Id);

            project.State = input.State;

            project.Name = input.Name;
            project.Description = input.Description;
            project.StartTime = input.StartTime;
            project.DeliverTime = input.DeliverTime;
            
            if (input.TeamLeaderId.HasValue)
            {
                project.TeamLeaderId = input.TeamLeaderId;
                var user = _userRepository.Get(ObjectMapper.Map<long>(input.TeamLeaderId));
                project.TeamLeader = user;
            }
        }
    }
}
