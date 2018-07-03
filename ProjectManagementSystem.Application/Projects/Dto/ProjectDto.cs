using Abp.Application.Services.Dto;
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

        public bool IsFinished { get; set; }


        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public override string ToString()
        {
            return null;
        }
    }
}
