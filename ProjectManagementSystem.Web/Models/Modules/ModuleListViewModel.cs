using ProjectManagementSystem.Modules.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjectManagementSystem.Web.Models.Modules
{
    public class ModuleListViewModel
    {
        /// <summary>
        /// 用来进行绑定列表过滤状态
        /// </summary>
        public bool? SelectedModuleState { get; set; }

        /// <summary>
        /// 列表展示
        /// </summary>
        public IReadOnlyList<ModuleDto> Modules { get; }

        /// <summary>
        /// 创建任务模型
        /// </summary>
        public CreateModuleDto CreateModuleInput { get; set; }

        /// <summary>
        /// 更新任务模型
        /// </summary>
        public UpdateModuleDto UpdateModuleInput { get; set; }

        public ModuleListViewModel(IReadOnlyList<ModuleDto> items)
        {
            Modules = items;
        }

        /// <summary>
        /// 用于过滤下拉框的绑定
        /// </summary>
        /// <returns></returns>

        public List<SelectListItem> GetModuleStateSelectListItems()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "AllTasks",
                    Value = "",
                    Selected = SelectedModuleState==null
                }
            };
        
            //list.AddRange(Enum.GetValues(typeof(bool))
            //    .Cast<bool>()
            //    .Select(state => new SelectListItem()
            //    {
            //        Text = $"TaskState_{state}",
            //        Value = state.ToString(),
            //        Selected = state == SelectedModuleState
            //    })
            //);
            return list;
        }
    }
}