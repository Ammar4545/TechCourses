using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechTree.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100,MinimumLength =2)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 2)]
        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
        public string LoginInValid { get; set; }
    }
}
