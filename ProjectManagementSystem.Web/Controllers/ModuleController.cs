using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using ProjectManagementSystem.Modules;
using ProjectManagementSystem.Projects;
using ProjectManagementSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ModuleController : AbpController
    {
        private readonly IModuleAppService _moduleAppService;
        private readonly IProjectAppService _projectAppService;
        private readonly IUserAppService _userAppService;

        public ModuleController(IProjectAppService projectAppService, IModuleAppService moduleAppService, IUserAppService userAppService)
        {
            _moduleAppService = moduleAppService;
            _projectAppService = projectAppService;
            _userAppService = userAppService;
        }
    }
}