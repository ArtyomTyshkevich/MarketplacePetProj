using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplacePetProj.Repositories
{
    public class OrderRepositories : IOrderRepositories
    {
        private readonly MarketDbContext _MarketDbContext;
        public OrderRepositories(MarketDbContext marketDbContext)
        {
            _MarketDbContext = marketDbContext;
        }

        public async Task Create(Order order)
        {
           await _MarketDbContext.orders.AddAsync(order);
           await _MarketDbContext.SaveChangesAsync();
        }

        public async Task Delete(Order order)
        {
            _MarketDbContext.orders.Remove(order);
            await _MarketDbContext.SaveChangesAsync();
        }

        public async Task<List<Order>> Get()
        {
            return await _MarketDbContext.orders.ToListAsync();
        }

        public async Task<Order?> Get (int Id)
        {
            return await _MarketDbContext.orders.Where(x=>x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task Update(Order order)
        {
            _MarketDbContext.orders.Update(order);
            await _MarketDbContext.SaveChangesAsync();
        }
    }
}
