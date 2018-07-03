using System.Threading.Tasks;
using Abp.Application.Services;
using ProjectManagementSystem.Sessions.Dto;

namespace ProjectManagementSystem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
