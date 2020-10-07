using System;
using System.Collections.Generic;

namespace FootballStatisticsManagementApp.Models
{
    public partial class Player
    {
        public Player()
        {
            Stats = new HashSet<Stats>();
        }

        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Dob { get; set; }
        public int KitNumber { get; set; }
        public int TeamId { get; set; }

        public Team Team { get; set; }
        public ICollection<Stats> Stats { get; set; }
    }
}
