using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Infrastructure.Models
{
    [Table("User")]
    public class User : BaseEntity
    {
        public User() : base()
        {
        }

        public User(string name, string email, string password) : base()
        {
            Name = name;
            Email = email;
            Password = password;
        }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("birthday")]
        public string Birthday { get; set; }

        [Column("languages")]
        public string Languages { get; set; }

        [Column("yearExperience")]
        public string YearExperience { get; set; }

        [Column("hobbies")]
        public string Hobbies { get; set; }

        [Column("description")]
        public string Description { get; set; }

        public virtual IList<UserSkill> UserSkills { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }
}

