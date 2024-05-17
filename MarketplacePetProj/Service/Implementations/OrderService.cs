using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using MarketplacePetProj.Repositories;
using MarketplacePetProj.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace MarketplacePetProj.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IClientService clientService;
        private readonly MarketDbContext marketDbContext;
        private readonly IOrderRepositories orderRepositories;
        public OrderService(IOrderRepositories orderRepositories, MarketDbContext marketDbContext, IClientService clientService)
        {
            this.orderRepositories = orderRepositories;
            this.marketDbContext = marketDbContext;
            this.clientService = clientService;
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
        public async Task<List<Order>> GetClientOrdersWithProduct(string userId)
        {
            return await marketDbContext.orders
                            .Where(o => o.CLientId == userId)
                            .Include(o => o.Products)
                            .Where(o => o.orderStatus != Enums.OrderStatus.basket)
                            .OrderByDescending(o => o.CreatedDate)
                            .ToListAsync();

        }
        public async Task<List<Order>> GetClientOwnProductWithOrders(string userId)
        {
            return await marketDbContext.orders
                .Where(o => o.orderStatus != Enums.OrderStatus.basket)
                .Include(o => o.Products
                    .Where(p => p.clientId == userId))
                .Where(o => o.Products.Any(p => p.clientId == userId))
                .OrderByDescending(o => o.CreatedDate)
                .ToListAsync();
        }
        public async Task<Order> GetBasketOrder(string userId)
        {
            var fullClient = await clientService.GetClientWithOrder(userId);
            var basketOrder = fullClient.Orders.FirstOrDefault(o => o.orderStatus == Enums.OrderStatus.basket);
            if (basketOrder == null)
            {
                basketOrder = new Order { orderStatus = Enums.OrderStatus.basket };
                fullClient.Orders.Add(basketOrder);
                await marketDbContext.SaveChangesAsync();
            }
            return basketOrder;
        }
    }
}
