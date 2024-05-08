using MarketplacePetProj.Models;
using MarketplacePetProj.Service.Interfaces;
using MarketplacePetProj.Repositories;
using MarketplacePetProj.Data;

namespace MarketplacePetProj.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepositories productRepositories;
        private readonly MarketDbContext marketDbContext;
        private readonly IWebHostEnvironment env;
        public ProductService(IProductRepositories productRepositories, MarketDbContext marketDbContext, IWebHostEnvironment env)
        {
            this.productRepositories = productRepositories;
            this.marketDbContext = marketDbContext;
            this.env = env;
        }
        public async Task CreateProduct(Product product)
        {
            await productRepositories.Create(product);
        }

        public async Task DeleteProduct(Product product)
        {
            string uploadFolder = Path.Combine(env.WebRootPath, "productImage", product.ImageFileName);
            System.IO.File.Delete(uploadFolder);
            await productRepositories.Delete(product);
            marketDbContext.SaveChanges();

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
