using MarketplacePetProj.Models;

namespace MarketplacePetProj.Service.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        Task DeleteOrder(Order order);
        Task<Order> GetOrder(int Id);
        Task<List<Order>> GetOrders();
        Task UpdateOrder(Order order);
    }
}