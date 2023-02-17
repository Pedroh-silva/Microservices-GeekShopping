using System.ComponentModel.DataAnnotations;

namespace GeekShopping.CouponAPI.Data.Data_Transfer_Objects
{
	public class CouponDTO
	{
		public int Id { get; set; }
		
		public string CouponCode { get; set; }
		
		public decimal DiscountAmount { get; set; }
	}
}
