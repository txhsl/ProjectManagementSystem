using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Modules.Dto
{
    [AutoMapTo(typeof(Module))]
    public class CreateModuleDto
    {
        public long? MemberId { get; set; }

        public int? ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TechStack { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime DeliverTime { get; set; }

        public int Level { get; set; }

    }
}
