using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using ProjectManagementSystem.Projects;
using ProjectManagementSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProjectController : AbpController
    {
        private readonly IProjectAppService _projectAppService;
        private readonly IUserAppService _userAppService;

        public ProjectController(IProjectAppService projectAppService, IUserAppService userAppService)
        {
            _projectAppService = projectAppService;
            _userAppService = userAppService;
        }
    }
}