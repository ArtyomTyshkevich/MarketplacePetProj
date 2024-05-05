using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using MarketplacePetProj.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using MarketplacePetProj.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MarketplacePetProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Client> userManager;
        private readonly MarketDbContext marketDbContext;
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly IClientService clientService;
        public HomeController(MarketDbContext marketDbContext, IProductService productService, UserManager<Client> userManager,
            IOrderService orderService, IClientService clientService)
        {
            this.marketDbContext = marketDbContext;
            this.productService = productService;
            this.userManager = userManager;
            this.orderService = orderService;
            this.clientService = clientService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(productService.GetProducts().Result);
        }
        [HttpGet("Basket/{id}")]
        public async Task<IActionResult> Basket(int id)
        {
            var userID =(await userManager.GetUserAsync(User)).Id;
            var clientWithOrders = await marketDbContext.clients
                                             .Where(c => c.Id == userID)
                                             .Include(c => c.Orders.Where(o => o.orderStatus == Enums.OrderStatus.basket))
                                             .ThenInclude(o => o.Products)
                                             .FirstOrDefaultAsync();
            var basketOrder = clientWithOrders.Orders.FirstOrDefault(o => o.orderStatus == Enums.OrderStatus.basket);

            if (basketOrder == null)
            {
                basketOrder = new Order { orderStatus = Enums.OrderStatus.basket };
                clientWithOrders.Orders.Add(basketOrder);
                await marketDbContext.SaveChangesAsync();
            }

            var product = await productService.GetProduct(id);

            if (!basketOrder.Products.Contains(product))
            {
                basketOrder.Products.Add(product);
                await marketDbContext.SaveChangesAsync();
            }

            return View(basketOrder.Products);
        }

        [HttpGet("Basket")]
        public async Task<IActionResult> Basket()
        {
            var userID = (await userManager.GetUserAsync(User)).Id;
            var clientWithOrders = await marketDbContext.clients
                                             .Where(c => c.Id == userID)
                                             .Include(c => c.Orders.Where(o => o.orderStatus == Enums.OrderStatus.basket))
                                             .ThenInclude(o => o.Products)
                                             .FirstOrDefaultAsync();
            var basketOrder = clientWithOrders.Orders.FirstOrDefault(o => o.orderStatus == Enums.OrderStatus.basket);
            return View(basketOrder.Products);
        }

        [HttpGet]
        public IActionResult ProductPage(int Id)
        {
            return View(productService.GetProduct(Id).Result);
        }
    }
}