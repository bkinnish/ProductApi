using System.Collections.Generic;

namespace ProductsApi
{
    // https://vmsdurano.com/asp-net-core-5-implement-web-api-pagination-with-hateoas-links/#defining-models-dtos
    public class PagedModel<TModel>
    {
        const int MaxPageSize = 500;
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IList<TModel> Items { get; set; }

        public PagedModel()
        {
            Items = new List<TModel>();
        }
    }
}
