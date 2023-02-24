using GeekShopping.CartAPI.Data.DataTransferObjects;

namespace GeekShopping.CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartDTO> FindCartByUserId(string UserId);
        Task<CartDTO> SaveOrUpdateCart(CartDTO cart);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ApllyCoupon(string UserId,string couponCode);
        Task<bool> RemoveCoupon(string UserId);
        Task<bool> ClearCart(string UserId);
        Task<bool> UpdateQuantity(CartDetailDTO dto);

	}
}
