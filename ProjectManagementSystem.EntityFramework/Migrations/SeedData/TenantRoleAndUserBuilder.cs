using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using ProjectManagementSystem.Authorization;
using ProjectManagementSystem.Authorization.Roles;
using ProjectManagementSystem.Authorization.Users;
using ProjectManagementSystem.EntityFramework;

namespace ProjectManagementSystem.Migrations.SeedData
{
    public class TenantRoleAndUserBuilder
    {
        private readonly ProjectManagementSystemDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(ProjectManagementSystemDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            //Admin role

            var adminRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true });
                _context.SaveChanges();

                //Grant all permissions to admin role
                var permissions = PermissionFinder
                    .GetAllPermissions(new ProjectManagementSystemAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();

                //Grant all project/module permissions
                var projectPermissions = PermissionFinder.GetAllPermissions(new ProjectModuleAuthorizationProvider()).ToList();
                permissions.AddRange(projectPermissions);

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRole.Id
                        });
                }

                _context.SaveChanges();
            }

            //Leader role for tenancy

            var leaderRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.TeamLeader);
            if (leaderRoleForHost == null)
            {
                leaderRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.TeamLeader, DisplayName = StaticRoleNames.Host.TeamLeader, IsStatic = true });
                _context.SaveChanges();

                //Grant all tenant permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new ProjectManagementSystemAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();


                //Grant several project/module permissions
                var projectPermissions = PermissionFinder.GetAllPermissions(new ProjectModuleAuthorizationProvider()).ToList();

                foreach (var permission in projectPermissions)
                {
                    if (permission.Name != PermissionNames.Pages_Projects && permission.Name != PermissionNames.Pages_Projects_EditOthers)
                    {
                        permissions.Add(permission);
                    }
                }

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = leaderRoleForHost.Id
                        });
                }

                _context.SaveChanges();
            }

            //Member role for tenancy

            var memberRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Member);
            if (memberRoleForHost == null)
            {
                memberRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.Member, DisplayName = StaticRoleNames.Host.Member, IsStatic = true });
                _context.SaveChanges();

                //Grant all tenant permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new ProjectManagementSystemAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();

                //Grant several project/module permissions
                var projectPermissions = PermissionFinder.GetAllPermissions(new ProjectModuleAuthorizationProvider()).ToList();

                foreach (var permission in projectPermissions)
                {
                    if (permission.Name == PermissionNames.Pages_Modules_EditState)
                    {
                        permissions.Add(permission);
                    }
                }

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = memberRoleForHost.Id
                        });
                }

                _context.SaveChanges();
            }


            //admin user

            var adminUser = _context.Users.FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == User.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com", User.DefaultPassword);
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }
        }
    }
}