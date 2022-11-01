using ProductsApi.Models.Products;
using System.Linq;

namespace ProductsApi.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ProductContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;   // DB has already been seeded.
            }

            var products = new Product[]
            {
                new Product { Name = "Apple", Price = 1.50m, Type = "fruit", Active = true },
                new Product { Name = "Orange", Price = 2.20m, Type = "fruit", Active = true },
                new Product { Name = "Pear", Price = 1.75m, Type = "fruit", Active = true },
                new Product { Name = "Banana", Price = 1.90m, Type = "fruit", Active = false },
                new Product { Name = "Pumpkin", Price = 3.00m, Type = "vegetable", Active = true },
                new Product { Name = "Potatoe", Price = 1.50m, Type = "vegetable", Active = true },
                new Product { Name = "Carrot", Price = 1.95m, Type = "vegetable", Active = true },
            };

            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();
        }
    }
}
