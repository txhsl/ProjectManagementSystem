using ProjectManagementSystem.Projects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjectManagementSystem.Web.Models.Projects
{
    public class ProjectListViewModel
    {
        /// <summary>
        /// 用来进行绑定列表过滤状态
        /// </summary>
        public bool? SelectedProjectState { get; set; }

        /// <summary>
        /// 列表展示
        /// </summary>
        public IReadOnlyList<ProjectDto> Projects { get; }

        /// <summary>
        /// 创建任务模型
        /// </summary>
        public CreateProjectDto CreateProjectInput { get; set; }

        /// <summary>
        /// 更新任务模型
        /// </summary>
        public UpdateProjectDto UpdateProjectInput { get; set; }

        public ProjectListViewModel(IReadOnlyList<ProjectDto> items)
        {
            Projects = items;
        }

        /// <summary>
        /// 用于过滤下拉框的绑定
        /// </summary>
        /// <returns></returns>

        public List<SelectListItem> GetProjectStateSelectListItems()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "AllTasks",
                    Value = "",
                    Selected = SelectedProjectState==null
                }
            };

            //list.AddRange(Enum.GetValues(typeof(bool))
            //    .Cast<bool>()
            //    .Select(state => new SelectListItem()
            //    {
            //        Text = $"TaskState_{state}",
            //        Value = state.ToString(),
            //        Selected = state == SelectedProjectState
            //    })
            //);
            return list;
        }
    }
}