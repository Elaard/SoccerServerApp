using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ModelsDTO
{
    public class TeamStatsDTO
    {
        public string TeamName { get; set; }
        public int? MatchesQuantity { get; set; }
        public int? Points { get; set; }
        public int? Wins { get; set; }
        public int? Loses { get; set; }
        public int? Draws { get; set; }
        public int? GoalsScored { get; set; }
        public int? GoalsLoses { get; set; }

    }
}
