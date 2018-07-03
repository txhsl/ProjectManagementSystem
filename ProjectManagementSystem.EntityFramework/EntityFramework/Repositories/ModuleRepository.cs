using Abp.EntityFramework;
using ProjectManagementSystem.IRepositories;
using ProjectManagementSystem.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.EntityFramework.Repositories
{
    class ModuleRepository : ProjectManagementSystemRepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(IDbContextProvider<ProjectManagementSystemDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public List<Module> GetModuleByMemberId(long memberId)
        {
            var query = GetAll();

            if (memberId > 0)
            {
                query = query.Where(t => t.MemberId == memberId);
            }

            return query.ToList();
        }

        public List<Module> GetModuleByProjectId(int projectId)
        {
            var query = GetAll();

            if (projectId > 0)
            {
                query = query.Where(t => t.ProjectId == projectId);
            }

            return query.ToList();
        }
    }
}
