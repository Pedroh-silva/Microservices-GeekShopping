using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.Models.ViewModel
{
    public class CartHeaderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        [Required]
        public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public DateTime DateTime { get; set; }
		[Required]
		public string Phone { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string CardNumber { get; set; }
		[Required]
		public string CVV { get; set; }
		[Required]
		public string ExpiryMonthYear { get; set; }
    }
}
