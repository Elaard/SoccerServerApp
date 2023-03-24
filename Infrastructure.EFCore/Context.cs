using Infrastructure.EFCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<MetaTeam> MetaTeams { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<DictionaryItem> DictionaryItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Team>()
                .HasMany(team => team.HomeMatches)
                .WithOne(home => home.HomeTeam)
                .HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Team>()
                .HasMany(team => team.AwayMatches)
                .WithOne(home => home.AwayTeam)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Team>()
                .HasOne<MetaTeam>(meta => meta.MetaTeam)
                .WithOne(team => team.Team)
                .HasForeignKey<MetaTeam>(meta => meta.TeamId);

            //League
            modelBuilder.Entity<League>()
                .HasIndex(x => x.Title)
                .IsUnique();

            modelBuilder.Entity<League>()
                .HasIndex(x => x.UrlForHtml)
                .IsUnique();

            //seed data
            Seed seed = new Seed(modelBuilder);
            seed.Add();

            base.OnModelCreating(modelBuilder);
        }
    }
}
