using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballStatisticsManagementApp.Models
{
    public partial class Team
    {
        public Team()
        {
            MatchAwayTeam = new HashSet<Match>();
            MatchHomeTeam = new HashSet<Match>();
            Player = new HashSet<Player>();
        }

        public int TeamId { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Name can only include letters")]
        public string Name { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Location can only include letters")]
        public string Location { get; set; }

        public ICollection<Match> MatchAwayTeam { get; set; }
        public ICollection<Match> MatchHomeTeam { get; set; }
        public ICollection<Player> Player { get; set; }
    }
}
