using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataModel.Enums;

namespace DataModel.Infrastructure.Models
{
    [Table("Skill")]
    public class Skill : BaseEntity
    {
        public Skill() : base()
        {
        }

        public Skill(string title)
        {
            Title = title;
        }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Column("domain")]
        public Domain Domain { get; set; }


        public virtual IList<UserSkill> UserSkills { get; set; }
    }
}

