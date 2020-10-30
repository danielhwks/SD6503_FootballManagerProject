using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballStatisticsManagementApp.Models
{
    public partial class League
    {
        public League()
        {
            Match = new HashSet<Match>();
        }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[0-9]+$",ErrorMessage = "Must input numbers in field")]
        public int LeagueId { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Incorrect data in field")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Year must be four digits long")]
        public string Year { get; set; }

        public ICollection<Match> Match { get; set; }
    }
}
