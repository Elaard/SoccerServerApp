using System.Collections.Generic;

namespace Infrastructure.EFCore.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string YearOfFundation { get; set; }
        public string Colors { get; set; }
        public string PageWWW { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }

        public int? CoachId { get; set; }
        public Coach Coach { get; set; }

        public MetaTeam MetaTeam { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Match> HomeMatches { get; set; }
        public ICollection<Match> AwayMatches { get; set; }
    }
}
