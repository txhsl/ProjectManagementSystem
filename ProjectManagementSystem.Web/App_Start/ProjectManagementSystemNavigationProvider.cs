using Abp.Application.Navigation;
using Abp.Localization;
using ProjectManagementSystem.Authorization;

namespace ProjectManagementSystem.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class ProjectManagementSystemNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "business",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "people",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "About",
                        icon: "info"
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.TaskList,
                        L("Task List"),
                        url: "",
                        icon: "menu",
                        requiresAuthentication: true
                    ).AddItem(
                        new MenuItemDefinition(
                        PageNames.ProjectList,
                        L("Project List"),
                        url: "Projects/Index",
                        requiresAuthentication: true
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                        PageNames.ModuleList,
                        L("Module List"),
                        url: "Modules/Index",
                        requiresAuthentication: true
                        )
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ProjectManagementSystemConsts.LocalizationSourceName);
        }
    }
}
