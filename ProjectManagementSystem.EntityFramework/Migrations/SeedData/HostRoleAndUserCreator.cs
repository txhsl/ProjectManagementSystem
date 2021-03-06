using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using ProjectManagementSystem.Authorization;
using ProjectManagementSystem.Authorization.Roles;
using ProjectManagementSystem.Authorization.Users;
using ProjectManagementSystem.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace ProjectManagementSystem.Migrations.SeedData
{
    public class HostRoleAndUserCreator
    {
        private readonly ProjectManagementSystemDbContext _context;

        public HostRoleAndUserCreator(ProjectManagementSystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.Admin, DisplayName = StaticRoleNames.Host.Admin, IsStatic = true });
                _context.SaveChanges();

                //Grant all tenant permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new ProjectManagementSystemAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                    .ToList();

                //Grant all project/module permissions
                var projectPermissions = PermissionFinder.GetAllPermissions(new ProjectModuleAuthorizationProvider()).ToList();
                permissions.AddRange(projectPermissions);

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRoleForHost.Id
                        });
                }

                _context.SaveChanges();
            }

            //Leader role for host

            var leaderRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.TeamLeader);
            if (leaderRoleForHost == null)
            {
                leaderRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.TeamLeader, DisplayName = StaticRoleNames.Host.TeamLeader, IsStatic = true });
                _context.SaveChanges();

                //Grant all tenant permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new ProjectManagementSystemAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                    .ToList();

                //Grant several project/module permissions
                var projectPermissions = PermissionFinder.GetAllPermissions(new ProjectModuleAuthorizationProvider()).ToList();

                foreach (var permission in projectPermissions)
                {
                    if(permission.Name != PermissionNames.Pages_Projects && permission.Name != PermissionNames.Pages_Projects_EditOthers)
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

            //Member role for host

            var memberRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Member);
            if (memberRoleForHost == null)
            {
                memberRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.Member, DisplayName = StaticRoleNames.Host.Member, IsStatic = true });
                _context.SaveChanges();

                //Grant all tenant permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new ProjectManagementSystemAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
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

            //Admin user for tenancy host

            var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(
                    new User
                    {
                        UserName = User.AdminUserName,
                        Name = "System",
                        Surname = "Administrator",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        IsEmailConfirmed = true,
                        Password = new PasswordHasher().HashPassword(User.DefaultPassword)
                    });

                _context.SaveChanges();

                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));

                _context.SaveChanges();
            }
        }
    }
}