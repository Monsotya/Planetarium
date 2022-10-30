using System.Data.Entity;
using PlanetariumService.Models;

namespace PlanetariumService.Database
{
    public class DbPlanetariumContext : DbContext
    {
        public DbSet<ErrorViewModel> ErrorViewModels { get; set; }
        public DbSet<HallUI> HallUIs { get; set; }
        public DbSet<OrdersUI> OrdersUIs { get; set; }
        public DbSet<PerformanceUI> PerformanceUIs { get; set; }
        public DbSet<PosterUI> PosterUIs { get; set; }
        public DbSet<TierUI> TierUIs { get; set; }
        public DbSet<TicketUI> TicketUIs { get; set; }
    }
}
