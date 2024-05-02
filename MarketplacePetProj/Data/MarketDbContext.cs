using MarketplacePetProj.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketplacePetProj.Data
{
    public class MarketDbContext : IdentityDbContext<Client>
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Client>clients { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}

