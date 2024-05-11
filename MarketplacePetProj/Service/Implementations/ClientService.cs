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
        private readonly IWebHostEnvironment env;
        public ClientService(IClientRepositories clientRepositories, MarketDbContext marketDbContext, IWebHostEnvironment env) 
        {
            this.marketDbContext = marketDbContext;
            this.clientRepositories = clientRepositories;
            this.env = env;
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
        public async Task UpdateClient(ClientDTO clientDTO)
        {
            var dbClient = await marketDbContext.clients.Where(c => c.Id == clientDTO.Id).FirstOrDefaultAsync();
            clientDTO.Name = clientDTO.Name ?? "";
            clientDTO.Description = clientDTO.Description ?? "";
            clientDTO.PhoneNum = clientDTO.PhoneNum ?? "";

            dbClient.Description = clientDTO.Description;
            dbClient.UserName = clientDTO.Name;
            dbClient.PhoneNumber = clientDTO.PhoneNum;
            if (null != clientDTO.ImageFile)
            {
                string uploadFolder = Path.Combine(env.WebRootPath, "ClientImages", dbClient.ImageName);
                System.IO.File.Delete(uploadFolder);
                uploadFolder = Path.Combine(env.WebRootPath, "ClientImages");
                var fileName = Guid.NewGuid().ToString() + "_" + clientDTO.ImageFile.FileName;
                string filepath = Path.Combine(uploadFolder, fileName);
                dbClient.ImageName = fileName;
                using (var stream = System.IO.File.Create(filepath))
                {
                    clientDTO.ImageFile.CopyTo(stream);
                }
            }
                await marketDbContext.SaveChangesAsync();
        }
        public async Task<Client?> GetClientWithOrder(string Id)
        {
            return await marketDbContext.clients
                            .Where(c => c.Id == Id)
                            .Include(c => c.Orders)
                            .ThenInclude(o => o.Products)
                            .FirstOrDefaultAsync();
        }
        public async Task<Client?> GetClientWithOwnProduct(string Id)
        {
            return await marketDbContext.clients
                            .Where(c => c.Id == Id)
                            .Include(c => c.CreatedProducts)
                            .FirstOrDefaultAsync();
        }
    }
}
