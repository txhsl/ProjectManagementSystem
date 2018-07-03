using ProjectManagementSystem.EntityFramework;
using ProjectManagementSystem.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Migrations.SeedData
{
    public class DefaultModuleCreator
    {
        private readonly ProjectManagementSystemDbContext _context;

        private static readonly List<Module> _modules;

        public DefaultModuleCreator(ProjectManagementSystemDbContext context)
        {
            _context = context;
        }

        static DefaultModuleCreator()
        {
            _modules = new List<Module>()
            {
                new Module("ABP BackEnd", "Use ABPZero to realize the bcakend of the system")
            };
        }

        public void Create()
        {
            foreach (var module in _modules)
            {
                if (_context.Modules.FirstOrDefault(t => t.Name == module.Name) == null)
                {
                    _context.Modules.Add(module);
                }
                _context.SaveChanges();
            }
        }
    }
}
