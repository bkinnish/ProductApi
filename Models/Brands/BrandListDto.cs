using System.Collections.Generic;

namespace ProductsApi.Models.Brands
{
    public class BrandListDto
    {
        public List<BrandDto> Items { get; set; }

        //public int ActivePage { get; set; }
        //public int MaxPages { get; set; }
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }
    }
}
