using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectManagementSystem.Projects.Dto
{
    public class ProjectSearchInputDto
    {
        public bool? IsFinished { get; set; }

        public int? TeamLeaderId { get; set; }
    }
}