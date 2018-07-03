using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using ProjectManagementSystem.Authorization.Users;
using ProjectManagementSystem.MultiTenancy;
using ProjectManagementSystem.Users;
using Microsoft.AspNet.Identity;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ProjectManagementSystemAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected ProjectManagementSystemAppServiceBase()
        {
            LocalizationSourceName = ProjectManagementSystemConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}