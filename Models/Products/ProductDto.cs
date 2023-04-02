
namespace ProductsApi.Models.Products
{
    /// <summary>
    /// Describes a product. (ie Apple, Orange, Pear, etc)
    /// </summary>
    public class ProductDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }

        public bool Active { get; set; }
    }
}
