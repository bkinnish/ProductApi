using ProductsApi.Models;
using ProductsApi.Models.Brands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsApi.Services.Brands
{
    public interface IBrandService
    {
        Task<BrandListDto> GetByPagedAsync(int limit, int page, string sortOrder, bool sortAsc, CancellationToken cancellationToken);
        Task<Brand> GetByIdAsync(int brandId);
        Task<Brand> CreateBrandAsync(Brand brand);
        Task<bool> UpdateAsync(int brandId, Brand brand);
        Task<bool> DeleteAsync(int brandId);
    }
}
