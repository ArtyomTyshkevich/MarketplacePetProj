using MarketplacePetProj.Enums;
using MarketplacePetProj.Models;
using Microsoft.AspNetCore.Identity;
namespace MarketplacePetProj.Models
{
    public class Client : IdentityUser
    {
        public string Description { get; set; } = "";

        public ICollection<Product> CreatedProducts { get; set; } = new List<Product>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ClientStatus ClientStatus { get; set; } 
    }
}