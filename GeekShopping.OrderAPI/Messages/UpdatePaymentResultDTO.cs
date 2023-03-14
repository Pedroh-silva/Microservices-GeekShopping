namespace GeekShopping.OrderAPI.Messages
{
	public class UpdatePaymentResultDTO
	{
		public int OrderId { get; set; }
		public bool Status { get; set; }
		public string Email { get; set; }
	}
}
