using MarketplacePetProj.Models;
using MarketplacePetProj.Repositories;
using MarketplacePetProj.Service.Interfaces;

namespace MarketplacePetProj.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepositories orderRepositories;
        public OrderService(IOrderRepositories orderRepositories)
        {
            this.orderRepositories = orderRepositories;
        }
        public async Task CreateProduct(Order order)
        {
            await orderRepositories.Create(order);
        }

        public async Task DeleteProduct(Order order)
        {
            await orderRepositories.Delete(order);
        }

        public async Task<Order> GetProduct(int Id)
        {
            return await orderRepositories.Get(Id);
        }

        public async Task<List<Order>> GetProducts()
        {
            return await orderRepositories.Get();
        }

        public async Task UpdateProduct(Order order)
        {
            await orderRepositories.Update(order);
        }
    }
}
