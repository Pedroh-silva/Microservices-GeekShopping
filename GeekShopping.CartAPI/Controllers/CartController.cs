﻿using GeekShopping.CartAPI.Data.DataTransferObjects;
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
        public async Task<ActionResult<CartDTO>> FindById(string userId)
        {
            var cart = await _repository.FindCartByUserId(userId);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpPost("add-cart/{id}")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO dto)
        {
            var cart = await _repository.SaveOrUpdateCart(dto);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpPut("update-cart/{id}")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO dto)
        {
            var cart = await _repository.SaveOrUpdateCart(dto);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpDelete("update-cart/{id}")]
        public async Task<ActionResult<CartDTO>> RemoveCart(int id)
        {
            var status = await _repository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

    }
}