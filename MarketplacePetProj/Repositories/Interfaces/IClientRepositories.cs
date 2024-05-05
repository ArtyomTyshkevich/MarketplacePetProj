using MarketplacePetProj.Models;

namespace MarketplacePetProj.Repositories
{
    public interface IClientRepositories 
    {
        public Task<List<Client>> Get();
        public Task<Client> Get(string Id);
        public Task Create(Client client);
        public Task Update(Client client);
        public Task Delete(Client client);

    }
}
