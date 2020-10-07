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

        public League League { get; set; }
        public ICollection<Stats> Stats { get; set; }
    }
}
