using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballStatisticsManagementApp.Models
{
    public class UserLogin
    {
        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[a-z]+$",ErrorMessage = "Please enter lowercase characters")]
        [StringLength(5, ErrorMessage = "Please enter no more than 5 characters")]
        [BindProperty]
        public string username { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[a-z]+$", ErrorMessage = "Please enter lowercase characters")]
        [StringLength(5, ErrorMessage = "Please enter no more than 5 characters")]
        [BindProperty]
        public string password { get; set; }
    }
}
