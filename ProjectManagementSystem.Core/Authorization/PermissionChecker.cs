using Abp.Authorization;
using ProjectManagementSystem.Authorization.Roles;
using ProjectManagementSystem.Authorization.Users;

namespace ProjectManagementSystem.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
