using System;

namespace Infrastructure.EFCore.Models
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public int? HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int? HomeTeamGoals { get; set; }

        public int? AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int? AwayTeamGoals { get; set; }

        public int QueueNumber { get; set; }

    }
}
