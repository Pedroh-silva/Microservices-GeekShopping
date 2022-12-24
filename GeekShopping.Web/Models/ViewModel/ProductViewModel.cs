using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GeekShopping.Web.Models.ViewModel
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Price { get; set; } = "";
		
		public string? Description { get; set; } = "";
		public string CategoryName { get; set; } = "";
		public string ImageURL { get; set; } = "";
	}
}
