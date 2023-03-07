﻿using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GeekShopping.OrderAPI.MessageConsumer
{
	public class RabbitMQCheckoutConsumer : BackgroundService
	{
		private readonly OrderRepository _repository;
		private IConnection _connection;
		private IModel _channel;

		public RabbitMQCheckoutConsumer(OrderRepository repository)
		{
			_repository = repository;
			var factory = new ConnectionFactory
			{
				HostName = "localhost",
				Password = "guest",
				UserName = "guest"
			};
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			stoppingToken.ThrowIfCancellationRequested();
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (channel, evt) =>
			{
				var content = Encoding.UTF8.GetString(evt.Body.ToArray());
				CheckoutHeaderDTO dto = JsonSerializer.Deserialize<CheckoutHeaderDTO>(content);
				ProcessOrder(dto).GetAwaiter().GetResult();
				_channel.BasicAck(evt.DeliveryTag, false);
			};
			_channel.BasicConsume("checkoutqueue", false,consumer);
			return Task.CompletedTask;	
		}

		private async Task ProcessOrder(CheckoutHeaderDTO dto)
		{
			OrderHeader order = new()
			{
				UserId = dto.UserId,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				OrderDetails = new List<OrderDetail>(),
				CardNumber = dto.CardNumber,
				CouponCode = dto.CouponCode,
				CVV = dto.CVV,
				DiscountAmount = dto.DiscountAmount,
				Email = dto.Email,
				ExpiryMonthYear = dto.ExpiryMonthYear,
				OrderTime = DateTime.Now,
				PurchaseAmount = dto.PurchaseAmount,
				PaymentStatus = false,
				Phone = dto.Phone,
				PurchaseDate = dto.DateTime
			};
			foreach (var details in dto.CartDetails)
			{
				OrderDetail detail = new()
				{
					ProductId = details.ProductId,
					ProductName = details.Product.Name,
					Price = details.Product.Price,
					Count = details.Count
				};
				order.CartTotalItens += details.Count;
				order.OrderDetails.Add(detail);
			}
			await _repository.AddOrder(order);
		}
	}
}
