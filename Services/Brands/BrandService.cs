using ProductsApi.Models.Brands;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;
using System.Threading.Tasks;
using ProductsApi.Extensions;
using System.Threading;

namespace ProductsApi.Services.Brands
{
    public class BrandService : IBrandService
    {
        private readonly ProductContext _context;

        public BrandService(ProductContext context)
        {
            _context = context;
        }

        // https://vmsdurano.com/asp-net-core-5-implement-web-api-pagination-with-hateoas-links/#defining-models-dtos
        public async Task<BrandListDto> GetByPagedAsync(int limit, int page, string sortOrder, bool sortAsc, CancellationToken cancellationToken)
        {


            // TODO: Check out alternate pagination: https://code-maze.com/paging-aspnet-core-webapi/


            var brandsQueryable = _context.Brands.AsNoTracking();

            if (sortOrder == "active")
                brandsQueryable = sortAsc ? brandsQueryable.OrderBy(p => p.Active) : brandsQueryable.OrderByDescending(p => p.Active);
            else
                brandsQueryable = sortAsc ? brandsQueryable.OrderBy(p => p.Name) : brandsQueryable.OrderByDescending(p => p.Name);

            var brands = await brandsQueryable.PaginateAsync(page, limit, cancellationToken);

            return new BrandListDto()
            {
                CurrentPage = brands.CurrentPage,
                TotalPages = brands.TotalPages,
                TotalItems = brands.TotalItems,
                Items = brands.Items.Select(p => new BrandDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Active = p.Active
                }).ToList()
            };
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _context.Brands.AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Brand> CreateBrandAsync(Brand brand)
        {
            if (brand.Id == 0)
            {
                brand.Id = null;
            }
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<bool> UpdateAsync(int brandId, Brand brand)
        {
            var existingBrand = await _context.Brands.FirstOrDefaultAsync(brand => brand.Id == brandId);
            if (existingBrand != null)
            {
                existingBrand.Name = brand.Name;
                existingBrand.Active = brand.Active;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingBrand = await _context.Brands.FindAsync(id);
            if (existingBrand != null)
            {
                _context.Brands.Remove(existingBrand);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
