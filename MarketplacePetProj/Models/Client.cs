using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MarketplacePetProj.Models
{
    public class Client : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = "";
        public string HashPasword { get; set; } = "";
        public ICollection<Product> CreatedProducts { get; set; }= new List<Product>();
        public ICollection<Order> Orders { get; set; }=new List<Order>();
        public Order basketOrder { get; set; } = new Order();
    }
}
