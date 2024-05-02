using MarketplacePetProj.Models;

namespace MarketplacePetProj.Service.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetProducts();
        public Task<Product> GetProduct(int Id);
        public Task CreateProduct(Product product);
        public Task UpdateProduct(Product product);
        public Task DeleteProduct(Product product);
        
    }
}


