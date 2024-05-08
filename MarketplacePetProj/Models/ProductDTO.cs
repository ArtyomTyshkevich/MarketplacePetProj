using MarketplacePetProj.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketplacePetProj.Models
{
    public class ProductDTO
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
