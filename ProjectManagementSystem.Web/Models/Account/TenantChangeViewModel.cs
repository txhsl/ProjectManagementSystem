using Abp.AutoMapper;
using ProjectManagementSystem.Sessions.Dto;

namespace ProjectManagementSystem.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}