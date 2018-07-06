using Abp.Application.Services;
using ProjectManagementSystem.Modules.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Modules
{
    public interface IModuleAppService : IApplicationService
    {

        ModuleSearchOutputDto SearchModules(ModuleSearchInputDto input);

        void UpdateModule(UpdateModuleDto input);

        int CreateModule(CreateModuleDto input);

        Task<ModuleDto> GetModuleByIdAsync(int moduleId);

        ModuleDto GetModuleById(int moduleId);

        void DeleteModule(int moduleId);

        void SendEmail(int id, string name);

        IList<ModuleDto> GetAllModules();
    }
}
