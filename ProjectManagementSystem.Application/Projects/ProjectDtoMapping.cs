using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectManagementSystem.Projects.Dto;

namespace ProjectManagementSystem.Projects
{
    public class ProjectDtoMapping : IDtoMapping
    {
        public void CreateMapping(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<CreateProjectDto, ProjectDto>();
            mapperConfig.CreateMap<UpdateProjectDto, ProjectDto>();
            mapperConfig.CreateMap<ProjectDto, UpdateProjectDto>();

            var projectDtoMapper = mapperConfig.CreateMap<Project, ProjectDto>();
            projectDtoMapper.ForMember(dto => dto.TeamLeaderName, map => map.MapFrom(m => m.TeamLeader.FullName));
        }
    }
}
