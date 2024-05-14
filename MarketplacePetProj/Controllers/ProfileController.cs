﻿using MarketplacePetProj.Data;
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
            var user = await userManager.GetUserAsync(User);
            var clientDto = new ClientDTO(user, env);
            return View(clientDto);
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
            var fullClient = await clientService.GetClientWithOwnProduct((await userManager.GetUserAsync(User)).Id);
            fullClient.ClientStatus = Enums.ClientStatus.Inactive;
            var products = fullClient.CreatedProducts.ToList();
            foreach (var product in products)
            {
                product.ProductStatus=Enums.ProductStatus.Inactive;
            }
            await marketDbContext.SaveChangesAsync();   
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UserOrderList()
        {
            var orders = await marketDbContext.orders
                .Where(o => o.CLientId == userManager.GetUserId(User))
                .Include(o => o.Products)
                .OrderByDescending(o => o.CreatedDate)
                .ToListAsync();

            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> UserBoughtProducts()
        {
            var userId = userManager.GetUserId(User);

            var orders = await marketDbContext.orders
                .Where(o=>o.orderStatus!= Enums.OrderStatus.basket)
                .Include(o => o.Products.Where(p => p.clientId == userId))
                .Where(o => o.Products.Any(p => p.clientId == userId))
                .OrderByDescending(o => o.CreatedDate)
                .ToListAsync();

            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> hideOrOpenProduct(int id)
        {
            var product = await productService.GetProduct(id);
            var clientStatus = product.ProductStatus;

            if (clientStatus == Enums.ProductStatus.Active)
                product.ProductStatus = Enums.ProductStatus.Inactive;
            else
                product.ProductStatus = Enums.ProductStatus.Active;

            await marketDbContext.SaveChangesAsync();
            return RedirectToAction("Profile", "Home");
        }
    }
}
