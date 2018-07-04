using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using ProjectManagementSystem.Authorization.Users;
using ProjectManagementSystem.Users.Dto;

namespace ProjectManagementSystem.Users
{
    public class UserDtoMapping : IDtoMapping
    {
        public void CreateMapping(IMapperConfigurationExpression mapperConfig)
        {
        }
    }
}