using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EFCore.Models
{
    public class MetaTeam
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
