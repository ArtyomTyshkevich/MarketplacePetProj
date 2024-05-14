using MarketplacePetProj.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketplacePetProj.Models
{
    public class ProductDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public IFormFile? ImageFile { get; set; }

        public ProductDTO(Product prod, IWebHostEnvironment env)
        {
            this.Id = prod.Id;
            this.Name = prod.Name;
            this.Price = prod.Price;
            this.Description = prod.Description;
            this.Quantity = prod.Quantity;
            string filePath = Path.Combine(env.WebRootPath, "productImage", prod.ImageFileName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                this.ImageFile = new FormFile(fileStream, 0, fileStream.Length, null, Path.GetFileName(filePath));
            }
        }
        public void nullCheck()
        {
            this.Name = this.Name ?? "";
            this.Description = this.Description ?? "";
        }
        public ProductDTO()
        {
        }
    }
}
