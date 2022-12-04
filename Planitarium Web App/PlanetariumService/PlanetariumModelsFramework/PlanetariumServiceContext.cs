using System.Data.Entity;

namespace PlanetariumModelsFramework
{
    public class PlanetariumServiceContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<PlanetariumServiceContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
