using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Infrastructure.Models
{
    public class LoginModel
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Display(Name = "Username")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }
    }
}
