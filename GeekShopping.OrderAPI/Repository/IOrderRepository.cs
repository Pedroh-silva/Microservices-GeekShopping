using GeekShopping.OrderAPI.Model;

namespace GeekShopping.OrderAPI.Repository
{
	public interface IOrderRepository
	{
		Task<bool> AddOrder(OrderHeader header);
		Task UpdateOrderPaymentStatus(int orderHeaderId,bool paid);

	}
}
