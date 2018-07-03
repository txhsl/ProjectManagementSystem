using Abp.Domain.Repositories;
using ProjectManagementSystem.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.IRepositories
{
    public interface IModuleRepository : IRepository<Module>
    {
        List<Module> GetModuleByMemberId(long memberId);
        List<Module> GetModuleByProjectId(int projectId);
    }
}
