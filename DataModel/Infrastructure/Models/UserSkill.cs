using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataModel.Enums;

namespace DataModel.Infrastructure.Models
{
    [Table("UserSkill")]
    public class UserSkill : BaseEntity
    {
        public UserSkill() : base()
        {
        }

        [Column("userId")]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [Column("skillId")]
        public Guid SkillId { get; set; }

        public virtual Skill Skill { get; set; }

        [Column("skillLevel")]
        public SkillLevel SkillLevel { get; set; }

        [Column("createdById")]
        public Guid CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }
    }
}

