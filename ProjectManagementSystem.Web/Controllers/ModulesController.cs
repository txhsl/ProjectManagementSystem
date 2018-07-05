using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using ProjectManagementSystem.Authorization.Users;
using ProjectManagementSystem.Modules;
using ProjectManagementSystem.Modules.Dto;
using ProjectManagementSystem.Projects;
using ProjectManagementSystem.Users;
using ProjectManagementSystem.Web.Models.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ModulesController : AbpController
    {
        private readonly IModuleAppService _moduleAppService;
        private readonly IProjectAppService _projectAppService;
        private readonly IUserAppService _userAppService;

        public ModulesController(IProjectAppService projectAppService, IModuleAppService moduleAppService, IUserAppService userAppService)
        {
            _moduleAppService = moduleAppService;
            _projectAppService = projectAppService;
            _userAppService = userAppService;
        }

        [ChildActionOnly]
        public PartialViewResult Create()
        {
            var userList = _userAppService.GetUsers().Result;
            ViewBag.MemberId = new SelectList(userList.Items, "Id", "UserName");
            var projectList = _projectAppService.SearchProjects(new Projects.Dto.ProjectSearchInputDto { });
            ViewBag.ProjectId = new SelectList(projectList.Projects, "Id", "Name");
            return PartialView("_CreateModule");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateModuleDto module)
        {
            var id = _moduleAppService.CreateModule(module);

            var input = new ModuleSearchInputDto();
            var output = _moduleAppService.SearchModules(input);

            return PartialView("_ListModules", output.Modules);
        }

        public PartialViewResult Edit(int id)
        {
            var module = _moduleAppService.GetModuleById(id);

            var updateModuleDto = AutoMapper.Mapper.Map<UpdateModuleDto>(module);

            var userList = _userAppService.GetUsers().Result;
            ViewBag.MemberId = new SelectList(userList.Items, "Id", "UserName", updateModuleDto.MemberId);
            var projectList = _projectAppService.SearchProjects(new Projects.Dto.ProjectSearchInputDto { });
            ViewBag.ProjectId = new SelectList(projectList.Projects, "Id", "Name", updateModuleDto.ProjectId);

            return PartialView("_EditModule", updateModuleDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateModuleDto updateModuleDto)
        {
            _moduleAppService.UpdateModule(updateModuleDto);

            var input = new ModuleSearchInputDto();
            var output = _moduleAppService.SearchModules(input);

            return PartialView("_ListModules", output.Modules);
        }

        public ActionResult Index(ModuleSearchInputDto input)
        {
            var output = _moduleAppService.SearchModules(input);

            var model = new ModuleListViewModel(output.Modules)
            {
                SelectedModuleState = input.State,
                SelectedUserId = input.MemberId,
                SelectedProjectId = input.ProjectId
            };

            var userList = _userAppService.GetUsers().Result;
            var userFullList = new List<User>{ new User { Id = -1, UserName = "All" } }.Concat<User>(userList.Items);
            ViewBag.SelectedUserId = new SelectList(userFullList, "Id", "UserName", model.SelectedUserId);

            var projectList = _projectAppService.SearchProjects(new Projects.Dto.ProjectSearchInputDto { });
            ViewBag.SelectedProjectId = new SelectList(projectList.Projects, "Id", "Name", model.SelectedProjectId);

            return View(model);
        }

        public PartialViewResult GetList(ModuleSearchInputDto input)
        {
            var output = _moduleAppService.SearchModules(input);
            return PartialView("_ListModules", output.Modules);
        }
    }
}