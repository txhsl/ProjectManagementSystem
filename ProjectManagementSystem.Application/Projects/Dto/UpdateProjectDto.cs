using Abp.AutoMapper;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Projects.Dto
{
    [AutoMapTo(typeof(Project))]
    public class UpdateProjectDto
    {
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }
        public long? TeamLeaderId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime DeliverTime { get; set; }

        public bool IsFinished { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public UpdateProjectDto()
        {
            LastModificationTime = Clock.Now;
        }
    }
}
