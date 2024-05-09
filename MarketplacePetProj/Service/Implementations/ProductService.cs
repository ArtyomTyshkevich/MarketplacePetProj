using MarketplacePetProj.Models;
using MarketplacePetProj.Service.Interfaces;
using MarketplacePetProj.Repositories;
using MarketplacePetProj.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MarketplacePetProj.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration configuration;
        private readonly IProductRepositories productRepositories;
        private readonly MarketDbContext marketDbContext;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<Client> userManager;
        public ProductService(IProductRepositories productRepositories, MarketDbContext marketDbContext, IWebHostEnvironment env, 
            UserManager<Client> userManager, IConfiguration configuration)
        {
            this.productRepositories = productRepositories;
            this.marketDbContext = marketDbContext;
            this.env = env;
            this.userManager = userManager;
            this.configuration = configuration;
        }
        public async Task CreateProduct(Product product)
        {
            await productRepositories.Create(product);
        }

        public async Task DeleteProduct(Product product)
        {
            if (product.ImageFileName != configuration.GetConnectionString("DefaultImageProductName"))
            {
                string uploadFolder = Path.Combine(env.WebRootPath, "productImage", product.ImageFileName);
                System.IO.File.Delete(uploadFolder);
            }
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

        public async Task UpdateProduct(ProductDTO productDTO)
        {
            var dbProduct = await marketDbContext.products.Where(p => p.Id == productDTO.Id).FirstOrDefaultAsync();
            dbProduct.Description= productDTO.Description;
            dbProduct.Name= productDTO.Name;
            dbProduct.Price= productDTO.Price;
            dbProduct.Quantity= productDTO.Quantity;

            if (productDTO.Quantity > 0)
                dbProduct.ProductStatus = Enums.ProductStatus.Active;
            if (null!= productDTO.ImageFile)
            {
                string uploadFolder = Path.Combine(env.WebRootPath, "productImage", dbProduct.ImageFileName);
                System.IO.File.Delete(uploadFolder);
                uploadFolder = Path.Combine(env.WebRootPath, "productImage");
                var fileName = Guid.NewGuid().ToString() + "_" + productDTO.ImageFile.FileName;
                string filepath = Path.Combine(uploadFolder, fileName);
                dbProduct.ImageFileName = fileName;
                using (var stream = System.IO.File.Create(filepath))
                {
                    productDTO.ImageFile.CopyTo(stream);
                }
                await marketDbContext.SaveChangesAsync();
            }
        }

        public async Task CreateProduct(ProductDTO productDTO, ClaimsPrincipal User)
        {
            Product prodObj = new Product()
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                Description = productDTO.Description,
                Quantity = productDTO.Quantity,
                CreatedDate = DateTime.Now,
                ProductStatus = Enums.ProductStatus.Active
            };
            string fileName = "";
            var userID = userManager.GetUserAsync(User).Result.Id;
            prodObj.clientId = userID;
            if (productDTO.ImageFile != null)
            {
                string uploadFolder = Path.Combine(env.WebRootPath, "productImage");
                fileName = Guid.NewGuid().ToString() + "_" + productDTO.ImageFile.FileName;
                string filepath = Path.Combine(uploadFolder, fileName);
                prodObj.ImageFileName = fileName;
                using (var stream = System.IO.File.Create(filepath))
                {
                    productDTO.ImageFile.CopyTo(stream);
                }
                marketDbContext.products.Add(prodObj);
                marketDbContext.SaveChanges();
            }
            else
            {
                prodObj.ImageFileName =configuration.GetConnectionString("DefaultImageProductName");
                marketDbContext.products.Add(prodObj);
                await marketDbContext.SaveChangesAsync();
            }

        }
    }

}
