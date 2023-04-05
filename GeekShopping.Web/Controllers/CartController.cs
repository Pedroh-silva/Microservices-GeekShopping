using GeekShopping.Web.Models.ViewModel;
using GeekShopping.Web.Services.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
	public class CartController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICartService _cartService;
		private readonly ICouponService _couponService;

		public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
		{
			_productService = productService;
			_cartService = cartService;
			_couponService = couponService;	
		}
		[Authorize]
		public async Task<IActionResult> CartIndex()
		{
			return View(await FindUserCart());
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Checkout()
		{
			return View(await FindUserCart());
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Checkout(CartViewModel model)
		{
			var token = await HttpContext.GetTokenAsync("access_token");

			var response = await _cartService.Checkout(model.CartHeader, token);
			if (response != null && response.GetType() == typeof(string))
			{
				TempData["Error"] = response;
				return RedirectToAction(nameof(Checkout));
			}
			else if (response != null)
			{
				return RedirectToAction(nameof(Confirmation));
			}
			return View(model);
		}
		[HttpGet]
		[Authorize]
		public  IActionResult Confirmation()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ApplyCoupon(CartViewModel model)
		{
			var token = await HttpContext.GetTokenAsync("access_token");
			var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
			var isValidCoupon = await _couponService.GetCoupon(model.CartHeader.CouponCode, token);
			if(isValidCoupon.CouponCode == null)
			{
				return NotFound(new { message = "Desculpe, cupom não encontrado!" });
			}
			var response = await _cartService.ApplyCoupon(model, token);
			if (response)
			{
				return NoContent();
			}
			return BadRequest(new { message = "Desculpe, algo deu errado ao aplicar o cupom!" });
		}
		[HttpDelete]
		public async Task<IActionResult> RemoveCoupon()
		{
			var token = await HttpContext.GetTokenAsync("access_token");
			var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

			var response = await _cartService.RemoveCoupon(userId, token);
			if (response)
			{
				return NoContent();
			}
			return BadRequest(new { message = "Desculpe, algo deu errado ao remover o cupom!" });
		}
		private async Task<CartViewModel> FindUserCart()
		{
			var token = await HttpContext.GetTokenAsync("access_token");
			var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

			var response = await _cartService.FindCartByUserId(userId, token);
			if (response?.CartHeader != null)
			{
				if (!string.IsNullOrEmpty(response.CartHeader.CouponCode))
				{
					var coupon = await _couponService.
						GetCoupon(response.CartHeader.CouponCode, token);
					if(coupon?.CouponCode != null)
					{
						response.CartHeader.DiscountAmount = coupon.DiscountAmount;
					}
				}
				foreach (var detail in response.CartDetails)
				{
					response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
				}
				response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
			}
			return response;
		}
		[HttpPost]
		public async Task<IActionResult> UpdateCount(int productId, string count)
		{
			var message = "Desculpe, algo deu errado ao tentar alterar o item do carrinho";
			int quantity;
			var isnumber = int.TryParse(count,out quantity);
			if(!isnumber || quantity <= 0)
			{
				return BadRequest(new { message });
			}
			var token = await HttpContext.GetTokenAsync("access_token");
			var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
			var cart = await _cartService.FindCartByUserId(userId, token);
			var cartDetail = cart.CartDetails.FirstOrDefault(cart=> cart.ProductId == productId);
			if(cartDetail == null)
			{
				return NotFound(new { message });
			}
			cartDetail.Count = quantity;
			var response = await _cartService.UpdateQuantity(cartDetail, token);
			if (response)
			{
				return NoContent();
			}
			return BadRequest(new { message });
		}
		[HttpDelete]
		public async Task<IActionResult> Remove(int id)
		{
			var token = await HttpContext.GetTokenAsync("access_token");
			var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

			var response = await _cartService.RemoveFromCart(id, token);
			if (response)
			{
				return NoContent();
			}
			return BadRequest(new { message = "Desculpe, algo deu errado ao tentar remover o item do carrinho" });
		}

	}
}