using Abp.AutoMapper;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Modules.Dto
{
    [AutoMapTo(typeof(Module))]
    public class UpdateModuleDto
    {
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

        public long? MemberId { get; set; }

        public int? ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TechStack { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime DeliverTime { get; set; }

        public int Level { get; set; }

        public ModuleState State { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public UpdateModuleDto()
        {
            LastModificationTime = Clock.Now;
        }
    }
}
