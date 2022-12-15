using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Infrastructure.Models
{
    [Table("Login")]
    public class Login : BaseEntity
    {
        public Login() : base()
        {
        }

        public Login(string token)
        {
            Token = token;
        }

        [Required]
        [Column("token")]
        public string Token { get; set; }
    }
}

