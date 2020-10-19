using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballStatisticsManagementApp.Models
{
    public partial class Match
    {
        public Match()
        {
            Stats = new HashSet<Stats>();
        }

        public int MatchId { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters to be inputed")]
        public string Location { get; set; }

        [Required]
        [RegularExpression("^(0?[1-9]|[12][0-9]|3[01])[\\/\\-](0?[1-9]|1[012])[\\/\\-]\\d{4}$", ErrorMessage = "Input Date in dd/mm/yyyy or dd-mm-yyyy format")]
        public string Date { get; set; }
        public int LeagueId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
        public League League { get; set; }
        public ICollection<Stats> Stats { get; set; }
    }
}
