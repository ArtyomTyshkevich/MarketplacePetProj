using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace MarketplacePetProj.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly MarketDbContext _MarketDbContext;
        public ProductRepositories(MarketDbContext marketDbContext)
        {
            _MarketDbContext = marketDbContext;
        }

        public async Task Create(Product product)
        {
            await _MarketDbContext.products.AddAsync(product);
            await _MarketDbContext.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _MarketDbContext.products.Remove(product);
            await _MarketDbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> Get()
        {
            return await _MarketDbContext.products.ToListAsync();
        }

        public async Task<Product?> Get(int Id)
        {
            return await _MarketDbContext.products.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task Update(Product product)
        {
            _MarketDbContext.products.Update(product);
            await _MarketDbContext.SaveChangesAsync();
        }
    }
}
