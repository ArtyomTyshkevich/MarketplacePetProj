using MarketplacePetProj.Models;
using System.Threading.Tasks;

namespace MarketplacePetProj.Service.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        Task DeleteOrder(Order order);
        Task<Order> GetOrder(int Id);
        Task<List<Order>> GetOrders();
        Task UpdateOrder(Order order);
        Task<List<Order>> GetClientOrdersWithProduct(string id);
        Task<List<Order>> GetClientOwnProductWithOrders(string userId);
        Task<Order> GetBasketOrder(string userId);
    }
}