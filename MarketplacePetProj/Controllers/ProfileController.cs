using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using MarketplacePetProj.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public ProfileController(MarketDbContext marketDbContext, IProductService productService, UserManager<Client> userManager,
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
        public async Task<IActionResult> EditCLient()
        {
            var user = await userManager.GetUserAsync(User);
            return View(new ClientDTO(user, env));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCLient(ClientDTO client)
        {
            await clientService.UpdateClient(client);
            return RedirectToAction("Profile");
        }
        public IActionResult DeleteCLient()
        {
            return View();
        }
    }
}
