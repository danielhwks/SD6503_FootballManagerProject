using System;
using System.Collections.Generic;

namespace FootballStatisticsManagementApp.Models
{
    public partial class Team
    {
        public Team()
        {
            Player = new HashSet<Player>();
        }

        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<Player> Player { get; set; }
    }
}
