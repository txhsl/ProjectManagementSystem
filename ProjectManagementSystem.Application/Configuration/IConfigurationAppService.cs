using System.Threading.Tasks;
using Abp.Application.Services;
using ProjectManagementSystem.Configuration.Dto;

namespace ProjectManagementSystem.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}