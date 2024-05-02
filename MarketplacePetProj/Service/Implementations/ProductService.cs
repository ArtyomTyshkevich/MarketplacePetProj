using MarketplacePetProj.Models;
using MarketplacePetProj.Service.Interfaces;
using MarketplacePetProj.Repositories;

namespace MarketplacePetProj.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepositories productRepositories;
        public ProductService(IProductRepositories productRepositories)
        {
            this.productRepositories = productRepositories;
        }
        public async Task CreateProduct(Product product)
        {
            await productRepositories.Create(product);
        }

        public async Task DeleteProduct(Product product)
        {
            await productRepositories.Delete(product);
        }

        public async Task<Product> GetProduct(int Id)
        {
            return await productRepositories.Get(Id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await productRepositories.Get();
        }

        public async Task UpdateProduct(Product product)
        {
           await productRepositories.Update(product);
        }
    }
}
