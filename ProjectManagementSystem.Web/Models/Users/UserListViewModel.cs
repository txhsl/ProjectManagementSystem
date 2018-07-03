using System.Collections.Generic;
using ProjectManagementSystem.Roles.Dto;
using ProjectManagementSystem.Users.Dto;

namespace ProjectManagementSystem.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}