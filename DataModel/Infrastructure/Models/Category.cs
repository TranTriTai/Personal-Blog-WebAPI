using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Infrastructure.Models
{
    [Table("Category")]
    public class Category : BaseEntity
    {
        public Category() : base()
        {
        }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        public virtual IList<Post> Posts { get; set; }

    }
}

