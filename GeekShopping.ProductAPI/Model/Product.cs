using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductAPI.Model
{
    public class Product
    {
        public int  Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";

        [Required]
        [Range(1, 10000)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = "";

        [StringLength(50)]
        public string CategoryName { get; set; } = "";

        [StringLength(300)]
        public string ImageURL { get; set; } = "";

    }
}
