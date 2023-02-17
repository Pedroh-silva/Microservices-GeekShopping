using GeekShopping.CouponAPI.Data.Data_Transfer_Objects;

namespace GeekShopping.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCouponCode(string couponCode); 
    }
}
