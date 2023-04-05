using System.ComponentModel.DataAnnotations;
namespace GeekShopping.Web.Models.ViewModel
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Price { get; set; }
		
		public string Description { get; set; }
		[Required]
		public string CategoryName { get; set; }
		[Required]
		public string ImageURL { get; set; }
	}
}
