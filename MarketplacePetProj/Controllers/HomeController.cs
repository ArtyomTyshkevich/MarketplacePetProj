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
            return View((await productService.GetProducts()).Where(p=>p.ProductStatus==Enums.ProductStatus.Active).ToList());
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

        [HttpGet]
        public async Task<IActionResult> ProductPage(int Id)
        {
            return View(await productService.GetProduct(Id));
        }
        [HttpGet("ProfileHome")]
        public async Task<IActionResult> ProfileHome()
        {
            var userID = (await userManager.GetUserAsync(User)).Id;
            var fullClient = await clientService.GetClientWithOwnProduct(userID);
            return View(fullClient);
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(ProductDTO obj)
        {
            Product prodObj = new Product() { Name = obj.Name, Price = obj.Price, Description = obj.Description, Quantity = obj.Quantity,
                CreatedDate= DateTime.Now, ProductStatus=Enums.ProductStatus.Active};
            string fileName = "";
            var userID = userManager.GetUserAsync(User).Result.Id;
            prodObj.clientId = userID;
            if (ModelState.IsValid && obj.ImageFile != null)
            {
                string uploadFolder = Path.Combine(env.WebRootPath, "productImage");
                fileName = Guid.NewGuid().ToString() + "_" + obj.ImageFile.FileName;
                string filepath = Path.Combine(uploadFolder, fileName);
                prodObj.ImageFileName = fileName;
                using (var stream = System.IO.File.Create(filepath))
                {
                    obj.ImageFile.CopyTo(stream);
                }

                marketDbContext.products.Add(prodObj);
                marketDbContext.SaveChanges();
                TempData["success"] = "Category Created successfully";
                return RedirectToAction("Index");
            }
            else
            {

            }
            return View(obj);

        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return View(await productService.GetProduct(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductPost(int id)
        {
            await productService.DeleteProduct(await productService.GetProduct(id));
            return RedirectToAction("Index");
        }

    }
}