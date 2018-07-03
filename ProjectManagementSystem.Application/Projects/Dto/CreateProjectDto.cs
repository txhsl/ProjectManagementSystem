using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Projects.Dto
{
    [AutoMapTo(typeof(Project))]
    public class CreateProjectDto
    {
        public long? TeamLeaderId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime DeliverTime { get; set; }

    }
}
