using MarketplacePetProj.Models;
using System.Security.Claims;

namespace MarketplacePetProj.Service.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetProducts();
        public Task<Product> GetProduct(int Id);
        public Task CreateProduct(Product product);
        public Task UpdateProduct(ProductDTO product);
        public Task DeleteProduct(Product product);
        public Task CreateProduct(ProductDTO productDTO, ClaimsPrincipal User);
        Task TransformStatus(int id);
    }
}


