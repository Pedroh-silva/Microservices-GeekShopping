using System.ComponentModel.DataAnnotations;

namespace GeekShopping.CouponAPI.Model
{
	public class Coupon
	{
		public int Id { get; set; }
		[Required]
		public string CouponCode { get; set; }
		[Required]
		public decimal DiscountAmount { get; set; }
	}
}
