using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectManagementSystem.MultiTenancy.Dto;

namespace ProjectManagementSystem.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
