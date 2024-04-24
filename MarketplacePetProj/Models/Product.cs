using MarketplacePetProj.Enums;
namespace MarketplacePetProj.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
