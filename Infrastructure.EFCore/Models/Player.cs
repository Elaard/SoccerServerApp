using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EFCore.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Goals { get; set; }
        public string Birth { get; set; }

        public int? PlayerPositionId { get; set; }
        public DictionaryItem PlayerPosition { get; set; }
    }
}
