﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Modules.Dto
{
    public class ModuleSearchInputDto
    {
        public ModuleState? State { get; set; }

        public int? ProjectId { get; set; }

        public int? MemberId { get; set; }
    }
}
