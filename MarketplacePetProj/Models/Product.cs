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
        [MaxLength(250)]
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        [Required]
        public ProductStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImageFileName { get; set; }
    }
}