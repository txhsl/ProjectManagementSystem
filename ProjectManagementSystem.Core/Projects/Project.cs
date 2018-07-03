using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using ProjectManagementSystem.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Projects
{
    public class Project : Entity, IHasCreationTime, IHasModificationTime
    {
        public const int MaxNameLength = 32;
        public const int MaxDescriptionLength = 64;

        public long? TeamLeaderId { get; set; }

        [ForeignKey("TeamLeaderId")]
        public User TeamLeader { get; set; }


        [Required]
        [MaxLength(MaxNameLength)]
        public string Name{ get;set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime DeliverTime { get; set; }
        public bool IsFinished { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public Project()
        {
            CreationTime = Clock.Now;
            IsFinished = false;
        }

        public Project(string name, string description = null) : this()
        {
            Name = name;
            Description = description;
        }
    }
}