using Abp.AutoMapper;
using Abp.Domain.Repositories;
using AutoMapper;
using ProjectManagementSystem.Modules;
using ProjectManagementSystem.Modules.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Projects
{
    public class ModuleAppService : ProjectManagementSystemAppServiceBase, IModuleAppService
    {

        private readonly IRepository<Module> _moduleRepository;

        public ModuleAppService(IRepository<Module> moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public ModuleSearchOutputDto SearchModules(ModuleSearchInputDto input)
        {
            var query = _moduleRepository.GetAll();

            if (input.MemberId.HasValue)
            {
                query = query.Where(t => t.MemberId == input.MemberId);
            }

            if (input.ProjectId.HasValue)
            {
                query = query.Where(t => t.ProjectId == input.ProjectId);
            }

            if (input.IsFinished.HasValue)
            {
                query = query.Where(t => t.IsFinished == input.IsFinished);
            }

            return new ModuleSearchOutputDto
            {
                Modules = Mapper.Map<List<ModuleDto>>(query.ToList())
            };
        }

        public int CreateModule(CreateModuleDto input)
        {
            Logger.Info("Creating a module for input: " + input);

            var module = new Module
            {
                Name = input.Name,
                Description = input.Description,
                StartTime = input.StartTime,
                DeliverTime = input.DeliverTime
            };

            return _moduleRepository.InsertAndGetId(module);
        }

        public void DeleteModule(int moduleId)
        {
            var module = _moduleRepository.Get(moduleId);
            if (module != null)
            {
                _moduleRepository.Delete(module);
            }
        }

        //not realized
        public IList<ModuleDto> GetAllModules()
        {
            var query = _moduleRepository.GetAll();

            throw new NotImplementedException();
        }

        public ModuleDto GetModuleById(int moduleId)
        {
            var module = _moduleRepository.Get(moduleId);

            return module.MapTo<ModuleDto>();
        }

        public async Task<ModuleDto> GetModuleByIdAsync(int moduleId)
        {
            var module = await _moduleRepository.GetAsync(moduleId);

            return module.MapTo<ModuleDto>();
        }

        public void UpdateModule(UpdateModuleDto input)
        {
            Logger.Info("Updating a module for input: " + input);

            var module = _moduleRepository.Get(input.Id);

            module.IsFinished = input.IsFinished;

            if (input.MemberId.HasValue)
            {
                module.MemberId = input.MemberId;
            }

            if (input.ProjectId.HasValue)
            {
                module.ProjectId = input.ProjectId;
            }
        }
    }
}
