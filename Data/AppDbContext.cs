using Microsoft.EntityFrameworkCore;
using inventoryApp.Models; 

namespace InventoryApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Gudang> Gudangs { get; set; }
        public DbSet<Barang> Barangs { get; set; }
    }
}
