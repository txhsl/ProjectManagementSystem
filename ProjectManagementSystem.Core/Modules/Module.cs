using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using ProjectManagementSystem.Authorization.Users;
using ProjectManagementSystem.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Modules
{
    public class Module : Entity, IHasCreationTime, IHasModificationTime
    {
        public const int MaxNameLength = 32;
        public const int MaxDescriptionLength = 64;
        public const int MaxTechStackLength = 32;

        public int? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public long? MemberId { get; set; }

        [ForeignKey("MemberId")]
        public User Member { get; set; }


        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(MaxTechStackLength)]
        public string TechStack { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime DeliverTime { get; set; }

        [Required]
        public int Level { get; set; }

        public bool IsFinished { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public Module()
        {
            CreationTime = Clock.Now;
            IsFinished = false;
        }

        public Module(string name, string description = null) : this()
        {
            Name = name;
            Description = description;
        }
    }
}
