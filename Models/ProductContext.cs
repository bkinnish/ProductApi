using Microsoft.EntityFrameworkCore;
using ProductsApi.Models.Brands;
using ProductsApi.Models.Products;

namespace ProductsApi.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null;
        public DbSet<Brand> Brands { get; set; } = null;
    }
}
