using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballStatisticsManagementApp.Models
{
    public partial class Stats
    {
        public int StatsId { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[0-9]\\d*$", ErrorMessage = "Please enter a positive number")]
        public int? Goals { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[0-9]\\d*$", ErrorMessage = "Please enter a positive number")]
        public int Assists { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[0-9]\\d*$", ErrorMessage = "Please enter a positive number")]
        public int Saves { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }

        public Match Match { get; set; }
        public Player Player { get; set; }
    }
}
