using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EFCore.Models
{
    public class DictionaryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActiv { get; set; }
        public int DictionaryId { get; set; }
        public Dictionary Dictionary { get; set; }
    }
}
