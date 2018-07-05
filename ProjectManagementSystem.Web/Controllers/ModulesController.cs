using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
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
            ViewBag.MemberId = new SelectList(userList.Items, "Id", "FullName");
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
            ViewBag.MemberId = new SelectList(userList.Items, "Id", "FullName", updateModuleDto.MemberId);
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
                SelectedModuleState = input.IsFinished

            };
            return View(model);
        }
    }
}