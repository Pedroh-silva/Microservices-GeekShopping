﻿using System.Reflection.PortableExecutable;
using AutoMapper;
using GeekShopping.CartAPI.Data.DataTransferObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
	public class CartRepository : ICartRepository
	{
		private readonly SQLContext _context;
		private IMapper _mapper;
		public CartRepository(SQLContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<bool> ApplyCoupon(string UserId, string couponCode)
		{
			var header = await _context.CartHeaders
					   .FirstOrDefaultAsync(c => c.UserId == UserId);
			if (header != null)
			{
				header.CouponCode = couponCode;
				_context.CartHeaders.Update(header);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> ClearCart(string UserId)
		{
			var cartHeader = await _context.CartHeaders
					   .FirstOrDefaultAsync(c => c.UserId == UserId);
			if (cartHeader != null)
			{
				_context.CartDetails
					.RemoveRange(
					_context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));
				_context.CartHeaders.Remove(cartHeader);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<CartDTO> FindCartByUserId(string userId)
		{
			Cart cart = new()
			{
				CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId) ?? new()
			};
			cart.CartDetails = _context.CartDetails
				.Where(c => c.CartHeaderId == cart.CartHeader.Id)
					.Include(c => c.Product);
			return _mapper.Map<CartDTO>(cart);
		}

		public async Task<bool> RemoveCoupon(string UserId)
		{
			var header = await _context.CartHeaders
					   .FirstOrDefaultAsync(c => c.UserId == UserId);
			if (header != null)
			{
				header.CouponCode = "";
				_context.CartHeaders.Update(header);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> RemoveFromCart(int cartDetailsId)
		{
			try
			{
				CartDetail cartDetail = await _context.CartDetails
					.FirstOrDefaultAsync(c => c.Id == cartDetailsId);
				int total = _context.CartDetails
					.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();

				_context.CartDetails.Remove(cartDetail);
				if (total == 1)
				{
					var cartHeaderToRemove = await _context.CartHeaders
						.FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
					_context.CartHeaders.Remove(cartHeaderToRemove);
				}
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{

				return false;
			}
		}
		public async Task<bool> UpdateQuantity(CartDetailDTO dto)
		{
			try
			{
				CartDetail cartDetailUpdated = _mapper.Map<CartDetail>(dto);
				var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(
						p => p.ProductId == cartDetailUpdated.ProductId &&
						p.CartHeaderId == cartDetailUpdated.CartHeaderId);
				cartDetail.Count = cartDetailUpdated.Count;
				_context.CartDetails.Update(cartDetail);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{

				return false;
			}

		}
		public async Task<CartDTO> SaveOrUpdateCart(CartDTO dto)
		{
			Cart cart = _mapper.Map<Cart>(dto);
			//Check if the product is already saved in database if it does not exist then save
			var product = await _context.Products.FirstOrDefaultAsync(
				p => p.Id == dto.CartDetails.FirstOrDefault().ProductId);
			if (product == null)
			{
				_context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
				await _context.SaveChangesAsync();
			}
			//Check if CartHeader is null
			var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
				c => c.UserId == cart.CartHeader.UserId);
			if (cartHeader == null)
			{
				//Create CartHeader and CartDetails
				_context.CartHeaders.Add(cart.CartHeader);
				await _context.SaveChangesAsync();
				cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
				cart.CartDetails.FirstOrDefault().Product = null;
				_context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
				await _context.SaveChangesAsync();

			}
			else
			{
				//if CartHeader is not null
				//Check if CartDetails has same product
				var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
					p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
					p.CartHeaderId == cartHeader.Id);
				if (cartDetail == null)
				{
					cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
					cart.CartDetails.FirstOrDefault().Product = null;
					_context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
					await _context.SaveChangesAsync();
				}
				else
				{
					cart.CartDetails.FirstOrDefault().Product = null;
					cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
					cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
					cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
					_context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
					await _context.SaveChangesAsync();
				}
			}
			return _mapper.Map<CartDTO>(cart);
		}
	}
}
