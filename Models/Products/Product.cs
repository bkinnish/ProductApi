using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsApi.Models.Products
{
    /// <summary>
    /// Describes a product. (ie Apple, Orange, Pear, etc)
    /// </summary>
    public class Product
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Type { get; set; }

        public bool Active { get; set; }
    }
}
