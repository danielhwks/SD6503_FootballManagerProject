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

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Only letters to be inputed")]
        public string Location { get; set; }

        // Validate that the input passes the regex check
        [Required]
        [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^
                          (?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$
                           |^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$",
                            ErrorMessage = "Input Date in dd/mm/yyyy or dd-mm-yyyy format")]
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
