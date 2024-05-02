using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MarketplacePetProj.Repositories;
using System.Linq;
using MarketplacePetProj.Service.Interfaces;

namespace MarketplacePetProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly MarketDbContext _MarketDbContext;
        private readonly IProductService productService;
        public HomeController(MarketDbContext marketDbContext, IProductService productService)
        {
            _MarketDbContext = marketDbContext;
            this.productService = productService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(productService.GetProducts().Result);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Backet(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Backet()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProductPage(int Id)
        {
            return View(productService.GetProducts().Result);
        }
    }
}