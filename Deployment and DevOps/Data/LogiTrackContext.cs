using Microsoft.EntityFrameworkCore;
using LogiTrack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LogiTrack.Data
{
        public class LogiTrackContext : IdentityDbContext<ApplicationUser>
    {
            public LogiTrackContext(DbContextOptions<LogiTrackContext> options)
                : base(options) {}

            public DbSet<InventoryItem> InventoryItems { get; set; }
            public DbSet<Order> Orders { get; set; }
    }
}
