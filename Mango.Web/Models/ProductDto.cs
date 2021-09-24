using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A product name is required")]
        public string Name { get; set; }

        [Range(1, 1000)]
        public double Price { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "A product category is required")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "A product image is required")]
        public string ImageUrl { get; set; }
    }
}
