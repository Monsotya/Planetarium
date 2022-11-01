using Microsoft.EntityFrameworkCore;

namespace PlanetariumModels
{
    public class PlanetariumServiceContext : DbContext
    {
        public static string Connection;

        public PlanetariumServiceContext(DbContextOptions<PlanetariumServiceContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Performance>().ToTable("Performances");
            modelBuilder.Entity<Poster>().ToTable("Posters");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<Tier>().ToTable("Tiers");
            modelBuilder.Entity<Orders>().ToTable("Orders");
            modelBuilder.Entity<Hall>().ToTable("Halls");
            modelBuilder.Entity<Users>().ToTable("Users");
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
