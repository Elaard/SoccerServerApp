using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EFCore.Models
{
    public class Dictionary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DictionaryItem> DictionaryItems { get; set; }
    }
}
