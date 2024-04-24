using MarketplacePetProj.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplacePetProj.Data
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Client>clients { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}

