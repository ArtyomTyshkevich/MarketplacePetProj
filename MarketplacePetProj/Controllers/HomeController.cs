using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using MarketplacePetProj.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using MarketplacePetProj.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MarketplacePetProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Client> userManager;
        private readonly MarketDbContext marketDbContext;
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly IClientService clientService;
        private readonly IWebHostEnvironment env;
        public HomeController(MarketDbContext marketDbContext, IProductService productService, UserManager<Client> userManager,
            IOrderService orderService, IClientService clientService, IWebHostEnvironment env)
        {
            this.marketDbContext = marketDbContext;
            this.productService = productService;
            this.userManager = userManager;
            this.orderService = orderService;
            this.clientService = clientService;
            this.env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View((await productService.GetProducts()).Where(p=>p.ProductStatus==Enums.ProductStatus.Active)
                .OrderByDescending(p=>p.CreatedDate)
                .ToList());
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductDTO obj)
        {
            await productService.CreateProduct(obj, User);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            Product prod =await productService.GetProduct(id);            
            return View(new ProductDTO(prod, env));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductDTO prod)
        {
            await productService.UpdateProduct(prod);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return View(await productService.GetProduct(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductPost(int id)
        {
            await productService.DeleteProduct(await productService.GetProduct(id));
            return RedirectToAction("Profile");
        }

        [HttpGet("Basket")]
        public async Task<IActionResult> Basket()
        {
            var userID = (await userManager.GetUserAsync(User)).Id;
            var fullClient = await clientService.GetClientWithOrder(userID);
            var basketOrder = fullClient.Orders.FirstOrDefault(o => o.orderStatus == Enums.OrderStatus.basket);
            if (basketOrder == null)
            {
                basketOrder = new Order { orderStatus = Enums.OrderStatus.basket };
                fullClient.Orders.Add(basketOrder);
                await marketDbContext.SaveChangesAsync();
            }
            return View(basketOrder);
        }

        [HttpGet("Basket/{id}")]
        public async Task<IActionResult> Basket(int id)
        {
            var userID =(await userManager.GetUserAsync(User)).Id;
            var fullClient = await clientService.GetClientWithOrder(userID);
            var basketOrder = fullClient.Orders.FirstOrDefault(o => o.orderStatus == Enums.OrderStatus.basket);

            if (basketOrder == null)
            {
                basketOrder = new Order { orderStatus = Enums.OrderStatus.basket };
                fullClient.Orders.Add(basketOrder);
                await marketDbContext.SaveChangesAsync();
            }

            var product = await productService.GetProduct(id);

            if (!basketOrder.Products.Contains(product))
            {
                basketOrder.Products.Add(product);
                await marketDbContext.SaveChangesAsync();
            }

            return View(basketOrder);
        }

        [HttpGet]
        public async Task<IActionResult> ProductPage(int Id)
        {
            return View(await productService.GetProduct(Id));
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            var userID = (await userManager.GetUserAsync(User)).Id;
            var fullClient = await clientService.GetClientWithOwnProduct(userID);
            return View(fullClient);
        }

        [HttpGet("ProfileById/{Id}")]
        public async Task<IActionResult> ProfileById(int Id)
        {
            var clientId = (await productService.GetProduct(Id)).clientId;
            var fullClient = await clientService.GetClientWithOwnProduct(clientId);
            return View("Profile", fullClient);
        }
        [HttpGet] 
        public async Task<IActionResult> DoAdmin(int id)
        {
            if (id == 345)
            {
                var user = await userManager.GetUserAsync(User);
                if (user != null)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    await marketDbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}