using System;
using System.Collections.Generic;

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
        public int LeagueId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
        public League League { get; set; }
        public ICollection<Stats> Stats { get; set; }
    }
}
