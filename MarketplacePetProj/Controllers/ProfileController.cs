using MarketplacePetProj.Data;
using MarketplacePetProj.Enums;
using MarketplacePetProj.Models;
using MarketplacePetProj.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MarketplacePetProj.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<Client> userManager;
        private readonly MarketDbContext marketDbContext;
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly IClientService clientService;
        private readonly IWebHostEnvironment env;
        private readonly SignInManager<Client> signInManager;
        public ProfileController(MarketDbContext marketDbContext, IProductService productService, UserManager<Client> userManager,
            IOrderService orderService, IClientService clientService, IWebHostEnvironment env, SignInManager<Client> signInManager)
        {
            this.marketDbContext = marketDbContext;
            this.productService = productService;
            this.userManager = userManager;
            this.orderService = orderService;
            this.clientService = clientService;
            this.env = env;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> EditCLient()
        {
            return View(new ClientDTO(await userManager.GetUserAsync(User), env));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCLient(ClientDTO client)
        {
            await clientService.UpdateClient(client);
            return RedirectToAction("Profile","Home");
        }
        [HttpGet]
        public IActionResult BannedClient(string Id)
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMyAccount()
        {
            return View(await userManager.GetUserAsync(User));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMyAccount(Client client)
        {
            clientService.DeleteClient(client);   
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UserOrderList()
        {
            return View(await orderService.GetClientOrdersWithProduct(userManager.GetUserId(User)));
        }
        [HttpGet]
        public async Task<IActionResult> UserBoughtProducts()
        {
            return View(await orderService.GetClientOwnProductWithOrders(userManager.GetUserId(User)));
        }
        [HttpGet]
        public async Task<IActionResult> hideOrOpenProduct(int id)
        {
            await productService.TransformStatus(id);
            return RedirectToAction("Profile", "Home");
        }
    }
}
