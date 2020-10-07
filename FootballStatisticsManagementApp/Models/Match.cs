using System;
using System.Collections.Generic;

namespace FootballStatisticsManagementApp.Models
{
    public partial class Match
    {
        public Match()
        {
            StatsNavigation = new HashSet<Stats>();
        }

        public int MatchId { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public int LeagueId { get; set; }
        public int StatsId { get; set; }

        public League League { get; set; }
        public Stats Stats { get; set; }
        public ICollection<Stats> StatsNavigation { get; set; }
    }
}
