using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using ProjectManagementSystem.Projects;
using ProjectManagementSystem.Projects.Dto;
using ProjectManagementSystem.Users;
using ProjectManagementSystem.Web.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProjectsController : AbpController
    {
        private readonly IProjectAppService _projectAppService;
        private readonly IUserAppService _userAppService;

        public ProjectsController(IProjectAppService projectAppService, IUserAppService userAppService)
        {
            _projectAppService = projectAppService;
            _userAppService = userAppService;
        }

        [ChildActionOnly]
        public PartialViewResult Create()
        {
            var userList = _userAppService.GetUsers().Result;
            ViewBag.TeamLeaderId = new SelectList(userList.Items, "Id", "FullName");
            return PartialView("_CreateProject");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProjectDto task)
        {
            var id = _projectAppService.CreateProject(task);

            var input = new ProjectSearchInputDto();
            var output = _projectAppService.SearchProjects(input);

            return PartialView("_ListProjects", output.Projects);
        }

        public PartialViewResult Edit(int id)
        {
            var project = _projectAppService.GetProjectById(id);

            var updateProjectDto = AutoMapper.Mapper.Map<UpdateProjectDto>(project);

            var userList = _userAppService.GetUsers().Result;
            ViewBag.TeamLeaderId = new SelectList(userList.Items, "Id", "FullName", updateProjectDto.TeamLeaderId);

            return PartialView("_EditProject", updateProjectDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateProjectDto updateProjectDto)
        {
            _projectAppService.UpdateProject(updateProjectDto);

            var input = new ProjectSearchInputDto();
            var output = _projectAppService.SearchProjects(input);

            return PartialView("_ListProjects", output.Projects);
        }

        public ActionResult Index(ProjectSearchInputDto input)
        {
            var output = _projectAppService.SearchProjects(input);

            var model = new ProjectListViewModel(output.Projects)
            {
                SelectedProjectState = input.IsFinished

            };
            return View(model);
        }
    }
}