using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ModelsDTO
{
    public class MatchQueueDTO
    {
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int? HomeTeamGoals { get; set; }
        public int? AwayTeamGoals { get; set; }
        public PrevMatchRoundDTO PrevMatch { get; set; }
    }
}
