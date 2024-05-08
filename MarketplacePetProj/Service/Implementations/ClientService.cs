using MarketplacePetProj.Models;
using MarketplacePetProj.Service.Interfaces;
using MarketplacePetProj.Repositories;
using MarketplacePetProj.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MarketplacePetProj.Service.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepositories clientRepositories;
        private readonly MarketDbContext marketDbContext;
        public ClientService(IClientRepositories clientRepositories, MarketDbContext marketDbContext) 
        {
            this.marketDbContext = marketDbContext;
            this.clientRepositories = clientRepositories;
        }
        public async Task CreateClient(Client client)
        {
            await clientRepositories.Create(client);
        }

        public async Task DeleteClient(Client client)
        {
            await clientRepositories.Delete(client);
        }

        public async Task<Client> GetClint(string Id)
        {
            return await clientRepositories.Get(Id);
        }

        public async Task<List<Client>> GetClients()
        {
            return await clientRepositories.Get();
        }

        public async Task UpdateClient(Client client)
        {
            await clientRepositories.Update(client);
        }
        public async Task<Client> GetClientWithOrder(string Id)
        {
            return await marketDbContext.clients
                            .Where(c => c.Id == Id)
                            .Include(c => c.Orders)
                            .ThenInclude(o => o.Products)
                            .FirstOrDefaultAsync();
        }
        public async Task<Client> GetClientWithOwnProduct(string Id)
        {
            return await marketDbContext.clients
                            .Where(c => c.Id == Id)
                            .Include(c => c.CreatedProducts)
                            .FirstOrDefaultAsync();
        }
    }
}
