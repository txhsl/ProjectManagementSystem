using Abp.Application.Services;
using ProjectManagementSystem.Projects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Projects
{
    public interface IProjectAppService : IApplicationService
    {

        ProjectSearchOutputDto SearchProjects(ProjectSearchInputDto input);

        void UpdateProject(UpdateProjectDto input);

        int CreateProject(CreateProjectDto input);

        Task<ProjectDto> GetProjectByIdAsync(int projectId);

        ProjectDto GetProjectById(int projectId);

        void DeleteProject(int projectId);

        IList<ProjectDto> GetAllProjects();
    }
}
