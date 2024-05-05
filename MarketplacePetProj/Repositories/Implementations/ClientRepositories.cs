using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace MarketplacePetProj.Repositories
{
    public class ClientRepositories : IClientRepositories
    {
        private readonly MarketDbContext _MarketDbContext;
        public ClientRepositories(MarketDbContext marketDbContext)
        {
            _MarketDbContext = marketDbContext;
        }

        public async Task Create(Client client)
        {
           await _MarketDbContext.clients.AddAsync(client);
           await _MarketDbContext.SaveChangesAsync();
        }

        public async Task Delete(Client client)
        {
            _MarketDbContext.clients.Remove(client);
            await _MarketDbContext.SaveChangesAsync();
        }

        public async Task<List<Client>> Get()
        {
            return await _MarketDbContext.clients.ToListAsync();
        }

        public async Task<Client?> Get(string userId)
        {
            return await _MarketDbContext.clients.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task Update(Client client)
        {
            _MarketDbContext.clients.Update(client);
            await _MarketDbContext.SaveChangesAsync();
        }
    }
}
