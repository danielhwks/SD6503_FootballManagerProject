using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballStatisticsManagementApp.Models
{
    public partial class Player
    {
        public Player()
        {
            Stats = new HashSet<Stats>();
        }

        public int PlayerId { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Name can only include letters")]
        public string Name { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^(0?[1-9]|[12][0-9]|3[01])[\\/\\-](0?[1-9]|1[012])[\\/\\-]\\d{4}$", ErrorMessage = "Input Date in dd/mm/yyyy or dd-mm-yyyy format")]
        public string Dob { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Must input numbers in field")]
        public int KitNumber { get; set; }
        public int TeamId { get; set; }

        public Team Team { get; set; }
        public ICollection<Stats> Stats { get; set; }
    }
}
