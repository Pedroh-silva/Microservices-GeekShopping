using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly DbContextOptions<SQLContext> _context;
		public OrderRepository(DbContextOptions<SQLContext> context)
		{
			_context = context;
		}
		public async Task<bool> AddOrder(OrderHeader header)
		{
			if(header == null) return false;
			await using var _db = new SQLContext(_context);
			_db.Headers.Add(header);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool status)
		{
			await using var _db = new SQLContext(_context);
			var header = await _db.Headers.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
			if(header != null)
			{
				header.PaymentStatus = status;
				await _db.SaveChangesAsync();
			}
		}
		
	}
}
