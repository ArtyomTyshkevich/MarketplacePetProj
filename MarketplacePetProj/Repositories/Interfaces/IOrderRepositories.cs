namespace MarketplacePetProj.Repositories.Interface
{
    public interface IOrder
    {
        public Task<Client> Get();
        public Task<Client> Get(int Id);
        public Task Create(Client client);
        public Task Update(Client client);
        public Task Delete(Client client);

    }
}