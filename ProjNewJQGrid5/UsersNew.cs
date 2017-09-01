//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjNewJQGrid5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class UsersNew
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email Id can not be blank...!")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        [Display(Name = "Email Id      :")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Password can not be blank...!")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 10)]
        [Display(Name = "Password      :")]
        public string Password { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        [Column(TypeName = "image")]
        [Display(Name = "Profile Image :")]
        public byte[] ProfileImg { get; set; }
    }
}
