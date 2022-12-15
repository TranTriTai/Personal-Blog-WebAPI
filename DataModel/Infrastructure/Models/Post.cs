using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Infrastructure.Models
{
    [Table("Post")]
    public class Post : BaseEntity
    {
        public Post() : base()
        {
        }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Column("tag")]
        public string Tag { get; set; }

        [Column("defaultImageUrl")]
        public string DefaultImageUrl { get; set; }

        [Column("readingDuration")]
        public string ReadingDuration { get; set; }

        [Column("ownerId")]
        public Guid OwnerId { get; set; }

        public virtual User Owner { get; set; }

        [Column("categoryId")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}

