using MarketplacePetProj.Data;
using MarketplacePetProj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MarketplacePetProj.Repositories;
using System.Linq;

namespace MarketplacePetProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly MarketDbContext _MarketDbContext;
        private readonly IProductRepositories productRepositories;
        public HomeController(MarketDbContext marketDbContext, IProductRepositories productRepositories)
        {
            _MarketDbContext = marketDbContext;
            this.productRepositories = productRepositories;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(productRepositories.Get().Result);
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
    }
}