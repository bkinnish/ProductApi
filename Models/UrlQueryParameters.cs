
namespace ProductsApi.Models
{
    public class UrlQueryParameters
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 50;

        public string SortOrder { get; set; }

        // Sort Ascending
        public bool sortAsc { get; set; }  
    }
}
