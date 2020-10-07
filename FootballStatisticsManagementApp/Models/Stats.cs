using System;
using System.Collections.Generic;

namespace FootballStatisticsManagementApp.Models
{
    public partial class Stats
    {
        public int StatsId { get; set; }
        public int? Goals { get; set; }
        public int Assists { get; set; }
        public int Saves { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }

        public Match Match { get; set; }
        public Player Player { get; set; }
    }
}
