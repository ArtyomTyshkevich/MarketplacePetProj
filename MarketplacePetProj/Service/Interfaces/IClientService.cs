using MarketplacePetProj.Models;

namespace MarketplacePetProj.Service.Interfaces
{
    public interface IClientService
    {
        Task CreateClient(Client client);
        Task DeleteClient(Client client);
        Order? FindBasketOrder(Client client);
        Task<List<Client>> GetClients();
        Task<Client> GetClint(string Id);
        Task UpdateClient(Client client);
    }
}