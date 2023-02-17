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

		public CartController(IProductService productService, ICartService cartService)
		{
			_productService = productService;
			_cartService = cartService;
		}
		[Authorize]
		public async Task<IActionResult> CartIndex()
		{
			return View(await FindUserCart());
		}
		private async Task<CartViewModel> FindUserCart()
		{
			var token = await HttpContext.GetTokenAsync("access_token");
			var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

			var response = await _cartService.FindCartByUserId(userId, token);
			if (response?.CartHeader != null)
			{
				foreach (var detail in response.CartDetails)
				{
					response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
				}
			}
			return response;
		}
		public async Task<IActionResult> UpdateCount(int productId, string count)
		{
			var message = "Desculpe, algo deu errado ao tentar alterar o item do carrinho";
			int quantity;
			var isnumber = int.TryParse(count,out quantity);
			if(!isnumber || quantity <= 0)
			{
				return Content(message);
			}
			var token = await HttpContext.GetTokenAsync("access_token");
			var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
			var cart = await _cartService.FindCartByUserId(userId, token);
			var cartDetail = cart.CartDetails.FirstOrDefault(cart=> cart.ProductId == productId);
			

			var responseRemove = await _cartService.RemoveFromCart(cartDetail.Id, token);
			if (responseRemove)
			{
				CartViewModel cartUpdated = new()
				{
					CartHeader = new CartHeaderViewModel
					{
						UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
					}
				};
				CartDetailViewModel cartDetailUpdated = new CartDetailViewModel()
				{
					Count = quantity,
					ProductId = productId,
					Product = await _productService.FindByIdProduct(productId, token)
				};
				List<CartDetailViewModel> cartDetails = new List<CartDetailViewModel>
				{
					cartDetailUpdated
				};
				cartUpdated.CartDetails = cartDetails;

				var response = await _cartService.AddItemToCart(cartUpdated, token);
				if (response != null)
				{
					return RedirectToAction(nameof(CartIndex));
				}
				return Content(message);
			}
			return Content(message);
		}
		public async Task<IActionResult> Remove(int id)
		{
			var token = await HttpContext.GetTokenAsync("access_token");
			var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

			var response = await _cartService.RemoveFromCart(id, token);
			if (response)
			{
				return RedirectToAction(nameof(CartIndex));
			}
			return Content("Desculpe, algo deu errado ao tentar remover o item do carrinho");
		}

	}
}
