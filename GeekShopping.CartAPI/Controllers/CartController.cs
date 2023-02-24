using GeekShopping.CartAPI.Data.DataTransferObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartRepository _repository;
        public CartController(ICartRepository repository)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));
        }
        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartDTO>> FindById(string id)
        {
            var cart = await _repository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpPost("add-cart")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO dto)
        {
            var cart = await _repository.SaveOrUpdateCart(dto);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpPut("update-cart")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO dto)
        {
            var cart = await _repository.SaveOrUpdateCart(dto);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
		[HttpPut("update-quantity")]
		public async Task<ActionResult<CartDTO>> UpdateQuantity(CartDetailDTO dto)
		{
			var status = await _repository.UpdateQuantity(dto);
			if (!status) return BadRequest();
			return Ok(status);
		}
		[HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartDTO>> RemoveCart(int id)
        {
            var status = await _repository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
		[HttpPost("apply-coupon")]
		public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO dto)
		{
			var status = await _repository.ApllyCoupon(dto.CartHeader.UserId, dto.CartHeader.CouponCode);
			if (!status) return NotFound();
			return Ok(status);
		}
        [HttpDelete("remove-coupon/{userId}")]
		public async Task<ActionResult<CartDTO>> RemoveCoupon(string userId)
		{
			var status = await _repository.RemoveCoupon(userId);
			if (!status) return NotFound();
			return Ok(status);
		}

	}
}
