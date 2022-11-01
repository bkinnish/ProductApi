using ProductsApi.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsApi.Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int productId);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> UpdateAsync(int productId, Product product);
        Task<bool> DeleteAsync(int productId);
    }
}
