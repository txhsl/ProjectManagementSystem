using Abp.Domain.Repositories;
using ProjectManagementSystem.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.IRepositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        List<Project> GetProjectByTeamLeaderId(long leaderId);
    }
}
