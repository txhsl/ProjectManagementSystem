using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectManagementSystem.Modules.Dto;

namespace ProjectManagementSystem.Modules
{
    public class ModuleDtoMapping : IDtoMapping
    {
        public void CreateMapping(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<CreateModuleDto, ModuleDto>();
            mapperConfig.CreateMap<UpdateModuleDto, ModuleDto>();
            mapperConfig.CreateMap<ModuleDto, UpdateModuleDto>();

            var moduleDtoMapper = mapperConfig.CreateMap<Module, ModuleDto>();
            moduleDtoMapper.ForMember(dto => dto.MemberName, map => map.MapFrom(m => m.Member.FullName));
            moduleDtoMapper.ForMember(dto => dto.ProjectName, map => map.MapFrom(m => m.Project.Name));
        }
    }
}
