using Microsoft.EntityFrameworkCore;
using SoccerProjectCase5.WebApi.Entities;

namespace SoccerProjectCase5.WebApi.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-N905OEG\\SQLEXPRESS;initial catalog=SoccerProjectDb;integrated security=true; trust server certificate=true");
        }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<MatchStatistic> MatchStatistics { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.HomeTeam)
                .WithMany()
                .HasForeignKey(f => f.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict); // Ev sahibi takımı silinince maçları otomatik SİLME

            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.AwayTeam)
                .WithMany()
                .HasForeignKey(f => f.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict); // Deplasman takımı silinince maçları otomatik SİLME

            base.OnModelCreating(modelBuilder);
        }
    }
}
