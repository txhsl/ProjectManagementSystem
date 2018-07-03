using Abp.EntityFramework;
using ProjectManagementSystem.IRepositories;
using ProjectManagementSystem.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.EntityFramework.Repositories
{
    public class ProjectRepository : ProjectManagementSystemRepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(IDbContextProvider<ProjectManagementSystemDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public List<Project> GetProjectByTeamLeaderId(long leaderId)
        {
            var query = GetAll();

            if(leaderId > 0)
            {
                query = query.Where(t => t.TeamLeaderId == leaderId);
            }

            return query.ToList();
        }
    }
}
