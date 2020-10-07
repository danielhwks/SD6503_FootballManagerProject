using System;
using System.Collections.Generic;

namespace FootballStatisticsManagementApp.Models
{
    public partial class Stats
    {
        public Stats()
        {
            Match = new HashSet<Match>();
        }

        public int StatsId { get; set; }
        public int? Goals { get; set; }
        public int Assists { get; set; }
        public int Saves { get; set; }
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }

        public Match MatchNavigation { get; set; }
        public Player Player { get; set; }
        public Team Team { get; set; }
        public ICollection<Match> Match { get; set; }
    }
}
