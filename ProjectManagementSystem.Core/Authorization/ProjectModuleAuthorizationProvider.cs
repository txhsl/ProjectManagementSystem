using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ProjectManagementSystem.Authorization
{
    public class ProjectModuleAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //Use for task dispatch
            context.CreatePermission(PermissionNames.Pages_Projects, L("Projects"));

            var modules = context.CreatePermission(PermissionNames.Pages_Modules, L("Modules"));
            modules.CreateChildPermission(PermissionNames.Pages_Modules_EditState, L("EditModulesState"));
            modules.CreateChildPermission(PermissionNames.Pages_Modules_EditOthers, L("EditModulesOthers"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ProjectManagementSystemConsts.LocalizationSourceName);
        }
    }
}