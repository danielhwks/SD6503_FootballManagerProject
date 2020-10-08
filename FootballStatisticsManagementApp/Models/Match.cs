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
        public string Location { get; set; }
        public string Date { get; set; }

        [Required]
        [RegularExpression("^([0-2][0-9]|(3)[0-1])(\\/)(((0)[0-9])|((1)[0-2]))(\\/)\\d{4}$", 
            ErrorMessage = "Date should be in dd-mm-yyyy format")]
        
        public int LeagueId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
        public League League { get; set; }
        public ICollection<Stats> Stats { get; set; }
    }
}
