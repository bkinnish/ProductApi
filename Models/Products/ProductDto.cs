using System.Collections.Generic;

namespace ProductsApi.Models.Products
{
    public class ProductDto
    {
        public List<Product> Products { get; set; }
        public int ActivePage { get; set; }
        public int MaxPages { get; set; }
    }
}
