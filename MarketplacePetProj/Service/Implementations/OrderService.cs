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
        public async Task CreateOrder(Order order)
        {
            await orderRepositories.Create(order);
        }

        public async Task DeleteOrder(Order order)
        {
            await orderRepositories.Delete(order);
        }

        public async Task<Order> GetOrder(int Id)
        {
            return await orderRepositories.Get(Id);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await orderRepositories.Get();
        }

        public async Task UpdateOrder(Order order)
        {
            await orderRepositories.Update(order);
        }
    }
}
