using MarketplacePetProj.Models;

namespace MarketplacePetProj.Service.Interfaces
{
    public interface IOrderService
    {
        Task CreateProduct(Order order);
        Task DeleteProduct(Order order);
        Task<Order> GetProduct(int Id);
        Task<List<Order>> GetProducts();
        Task UpdateProduct(Order order);
    }
}