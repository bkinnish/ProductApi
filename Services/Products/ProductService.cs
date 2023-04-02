using ProductsApi.Models.Products;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;
using System.Threading.Tasks;
using ProductsApi.Extensions;
using System.Threading;

namespace ProductsApi.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;

        public ProductService(ProductContext context)
        {
            _context = context;
        }

        // https://vmsdurano.com/asp-net-core-5-implement-web-api-pagination-with-hateoas-links/#defining-models-dtos
        public async Task<ProductListDto> GetByPagedAsync(int limit, int page, string sortOrder, bool sortAsc, CancellationToken cancellationToken)
        {


            // TODO: Check out alternate pagination: https://code-maze.com/paging-aspnet-core-webapi/


            var productsQueryable = _context.Products.AsNoTracking();

            if (sortOrder == "price")
                productsQueryable = sortAsc ? productsQueryable.OrderBy(p => p.Price) : productsQueryable.OrderByDescending(p => p.Price);
            else if (sortOrder == "type")
                productsQueryable = sortAsc ? productsQueryable.OrderBy(p => p.Type) : productsQueryable.OrderByDescending(p => p.Type);
            else if (sortOrder == "active")
                productsQueryable = sortAsc ? productsQueryable.OrderBy(p => p.Active) : productsQueryable.OrderByDescending(p => p.Active);
            else
                productsQueryable = sortAsc ? productsQueryable.OrderBy(p => p.Name) : productsQueryable.OrderByDescending(p => p.Name);

            var products = await productsQueryable.PaginateAsync(page, limit, cancellationToken);

            return new ProductListDto()
            {
                CurrentPage = products.CurrentPage,
                TotalPages = products.TotalPages,
                TotalItems = products.TotalItems,
                Items = products.Items.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Type = p.Type,
                    Active = p.Active
                }).ToList()


                // TODO: Unit tests
                //var products = new List<Product>
                //{
                //    new Product { Id = 1, Name = "Apple", Price = 1.50m, Type = "fruit", Active = true },
                //    new Product { Id = 2, Name = "Orange", Price = 2.20m, Type = "fruit", Active = true },
                //    new Product { Id = 3, Name = "Pear", Price = 1.75m, Type = "fruit", Active = true },
                //    new Product { Id = 4, Name = "Banana", Price = 1.90m, Type = "fruit", Active = false },
                //    new Product { Id = 5, Name = "Pumpkin", Price = 3.00m, Type = "vegetable", Active = true }
                //};
                //return products;
            };
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            if (product.Id == 0)
            {
                product.Id = null;
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(int productId, Product product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Type = product.Type;
                existingProduct.Price = product.Price;
                existingProduct.Active = product.Active;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct != null)
            {
                _context.Products.Remove(existingProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
