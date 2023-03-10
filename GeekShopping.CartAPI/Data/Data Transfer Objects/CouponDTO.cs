namespace GeekShopping.CartAPI.Data.DataTransferObjects
{
	public class CouponDTO
	{
		public int Id { get; set; }
		
		public string CouponCode { get; set; }
		
		public decimal DiscountAmount { get; set; }
	}
}
