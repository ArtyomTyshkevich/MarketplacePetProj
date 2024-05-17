using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using MarketplacePetProj.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketplacePetProj.Controllers
{
    public class BasketController : Controller
    {
        private readonly UserManager<Client> userManager;
        private readonly MarketDbContext marketDbContext;
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly IClientService clientService;
        public BasketController(MarketDbContext marketDbContext, IProductService productService, UserManager<Client> userManager,
            IOrderService orderService, IClientService clientService)
        {
            this.marketDbContext = marketDbContext;
            this.productService = productService;
            this.userManager = userManager;
            this.orderService = orderService;
            this.clientService = clientService;
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var basketOrder = await orderService.GetBasketOrder(userManager.GetUserId(User));
            basketOrder.Products.Remove(await productService.GetProduct(Id));
            await marketDbContext.SaveChangesAsync();
            return RedirectToAction("Basket", "Home");
        }
        public async Task<IActionResult> PlaceOrder(int id)
        {
            var basketOrder = await marketDbContext.orders
                                       .Where(o => o.Id == id)
                                       .Include(p => p.Products)
                                       .FirstOrDefaultAsync();
            basketOrder.orderStatus = Enums.OrderStatus.processed;
            basketOrder.CreatedDate = DateTime.Now;
            await marketDbContext.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }
    }
}
