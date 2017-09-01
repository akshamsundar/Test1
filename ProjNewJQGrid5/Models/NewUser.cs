using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjNewJQGrid5.Models
{
    public class NewUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string EmailId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength=6)]
        public string Password { get; set; }

        [Required]
        public string PasswordSalt { get; set; }
    }
}