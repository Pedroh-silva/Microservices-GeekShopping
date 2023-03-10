using GeekShopping.CartAPI.Data.DataTransferObjects;

namespace GeekShopping.CartAPI.Repository
{
	public interface ICouponRepository
    {
        Task<CouponDTO> GetCoupon(string couponCode, string token); 
    }
}
