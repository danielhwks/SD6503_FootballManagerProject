using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogin.Models
{
    public class UserLogin
    {
        [Required]
        [RegularExpression("^[a-z]+$",ErrorMessage = "Please enter lowercase characters")]
        [StringLength(5, ErrorMessage = "Please enter no more than 5 characters")]
        public string username { get; set; }

        [Required]
        [RegularExpression("^[a-z]+$", ErrorMessage = "Please enter lowercase characters")]
        [StringLength(5, ErrorMessage = "Please enter no more than 5 characters")]
        public string password { get; set; }


    }
}
