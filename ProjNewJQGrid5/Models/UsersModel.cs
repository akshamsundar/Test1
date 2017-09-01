using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjNewJQGrid5.Models
{
    public class UsersModel
    {
        private MainDbContext Db=new MainDbContext();

        [Required]
        [EmailAddress]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Enter Email Id      :")]
        
        public string EmailId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength=6)]
        [Display(Name = "Enter your Password :")]        
        public string Password { get; set; }

    }
}