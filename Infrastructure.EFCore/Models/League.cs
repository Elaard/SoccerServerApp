using System.Collections.Generic;

namespace Infrastructure.EFCore.Models
{
    public class League
    {
        public int Id { get; set; }
        public string UrlForHtml { get; set; }
        public string Title { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
