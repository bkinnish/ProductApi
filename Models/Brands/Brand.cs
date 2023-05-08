using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models.Brands
{
    /// <summary>
    /// Describes a brand. (ie Coles, Woolies,  etc)
    /// </summary>
    public class Brand
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
