﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Projects.Dto
{
    public class ProjectDto : EntityDto
    {
        public long? TeamLeaderId { get; set; }

        public string TeamLeaderName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime DeliverTime { get; set; }

        public ProjectState State { get; set; }


        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public override string ToString()
        {
            return null;
        }

        /// <summary>
        /// 根据任务状态，获取定义的css样式
        /// </summary>
        /// <returns></returns>
        public string GetTaskLable()
        {
            string style = "";

            switch (State)
            {
                case ProjectState.Pending:
                    style = "fa-spinner";
                    break;
                case ProjectState.Open:
                    style = "fa-spinner fa-spin ";
                    break;
                case ProjectState.Completed:
                    style = "fa-check-circle ";
                    break;
            }
            return style;

        }
    }
}
