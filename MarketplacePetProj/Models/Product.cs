using MarketplacePetProj.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketplacePetProj.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = "";

        [Required]
        public decimal Price { get; set; }

        [MaxLength(700)]
        public string Description { get; set; } = "";

        public int Quantity { get; set; }

        [Required]
        public ProductStatus ProductStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ImageFileName { get; set; }

        // Список заказов, в которых участвует этот продукт
        public ICollection<Order> Orders { get; set; }
        public string? clientId { get; set; }
    }
}