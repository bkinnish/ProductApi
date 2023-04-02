using ProductsApi.Models;
using ProductsApi.Models.Products;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsApi.Services.Products
{
    public interface IProductService
    {
        Task<ProductListDto> GetByPagedAsync(int limit, int page, string sortOrder, bool sortAsc, CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(int productId);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> UpdateAsync(int productId, Product product);
        Task<bool> DeleteAsync(int productId);
    }
}
