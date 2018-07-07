using System.Linq;
using ProjectManagementSystem.EntityFramework;
using ProjectManagementSystem.MultiTenancy;

namespace ProjectManagementSystem.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly ProjectManagementSystemDbContext _context;

        public DefaultTenantCreator(ProjectManagementSystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }

            var team1Tenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == "Team1");
            if (team1Tenant == null)
            {
                _context.Tenants.Add(new Tenant { TenancyName = "Team1", Name = "Team1" });
                _context.SaveChanges();
            }

            var team2Tenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == "Team2");
            if (team2Tenant == null)
            {
                _context.Tenants.Add(new Tenant { TenancyName = "Team2", Name = "Team2" });
                _context.SaveChanges();
            }
        }
    }
}
