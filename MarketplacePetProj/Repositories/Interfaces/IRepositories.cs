using MarketplacePetProj.Models;

namespace MarketplacePetProj.Repositories
{
    public interface IRepositories<T>
    {
        public Task<List<T>> Get();
        public Task<T> Get(int Id);
        public Task Create(T client);
        public Task Update(T client);
        public Task Delete(T client);
    }
}
