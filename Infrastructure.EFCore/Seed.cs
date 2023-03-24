using Infrastructure.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace Infrastructure.EFCore
{
    public class Seed
    {
        private readonly ModelBuilder modelBuilder;

        public Seed(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Add()
        {
            Dictionary();
            DictionaryItems();
        }
        private void Dictionary()
        {
            modelBuilder.Entity<Dictionary>().HasData(
               new Dictionary { Id = 1, Name = "PlayerPosition", });
        }
        private void DictionaryItems()
        {
            modelBuilder.Entity<DictionaryItem>().HasData(
               new DictionaryItem { Id = 1, Name = "Napastnik", DictionaryId=1},
               new DictionaryItem { Id = 2, Name = "Bramkarz", DictionaryId=1},
               new DictionaryItem { Id = 3, Name = "Ofensywny pomocnik", DictionaryId=1},
               new DictionaryItem { Id = 4, Name = "Defensywny pomocnik", DictionaryId=1});
        }
    }
}
