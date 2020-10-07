using System;
using System.Collections.Generic;

namespace FootballStatisticsManagementApp.Models
{
    public partial class League
    {
        public League()
        {
            Match = new HashSet<Match>();
        }

        public int LeagueId { get; set; }
        public string Year { get; set; }

        public ICollection<Match> Match { get; set; }
    }
}
