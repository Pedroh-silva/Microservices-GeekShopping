using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repository
{
	public class EmailRepository : IEmailRepository
	{
		private readonly DbContextOptions<SQLContext> _context;
		public EmailRepository(DbContextOptions<SQLContext> context)
		{
			_context = context;
		}

		public async Task LogEmail(UpdatePaymentResultMessage message)
		{
			{
				EmailLog emailLog = new EmailLog()
				{
					Email = message.Email,
					SentDate = DateTime.Now,
					Log = $"Pedido - {message.OrderId} foi criado com sucesso!"
				};
				await using var _db = new SQLContext(_context);
				_db.EmailLogs.Add(emailLog);
				await _db.SaveChangesAsync();
			}

		}
	}
}