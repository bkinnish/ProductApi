using ProductsApi.Models.Products;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;
using System.Threading.Tasks;

namespace ProductsApi.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;

        public ProductService(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.OrderBy(p => p.Name).AsNoTracking().ToListAsync();

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
