using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Modules.Dto
{
    public class ModuleDto : EntityDto
    {
        public long? MemberId { get; set; }

        public string MemberName { get; set; }

        public int? ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TechStack { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime DeliverTime { get; set; }

        public int Level { get; set; }

        public bool IsFinished { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public override string ToString()
        {
            return null;
        }
    }
}
