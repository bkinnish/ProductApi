using System.Collections.Generic;

namespace ProductsApi.Models.Products
{
    public class ProductListDto
    {
        public List<ProductDto> Items { get; set; }
        //public int ActivePage { get; set; }
        //public int MaxPages { get; set; }
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }
    }
}
