using ProjectManagementSystem.EntityFramework;
using ProjectManagementSystem.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Migrations.SeedData
{
    public class DefaultProjectCreator
    {
        private readonly ProjectManagementSystemDbContext _context;

        private static readonly List<Project> _projects;

        public DefaultProjectCreator(ProjectManagementSystemDbContext context)
        {
            _context = context;
        }

        static DefaultProjectCreator()
        {
            _projects = new List<Project>()
            {
                new Project(".Net Course Design", "A project management system")
            };
        }

        public void Create()
        {
            foreach(var project in _projects)
            {
                if (_context.Projects.FirstOrDefault(t => t.Name == project.Name) == null)
                {
                    _context.Projects.Add(project);
                }
                _context.SaveChanges();
            }
        }
    }
}
