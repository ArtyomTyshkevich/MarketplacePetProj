using System.ComponentModel.DataAnnotations;

namespace MarketplacePetProj.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = "";
        public string HashPasword { get; set; } = "";
        public List<Product> CreatedProducts { get; set; }
        public Order Order { get; set; }
        public Client()
        {
            CreatedProducts = new List<Product>();
            Order = new Order();
        }
    }
}
