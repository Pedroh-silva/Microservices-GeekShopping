using GeekShopping.Web.Models;
using System.Net.Http.Headers;
using GeekShopping.Web.Models.ViewModel;
using GeekShopping.Web.Services.IService;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        private const string basePath = "api/v1/Cart";

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{basePath}/find-cart/{userId}");
            return await response.ReadContentAs<CartViewModel>();
        }
        public async Task<CartViewModel> AddItemToCart(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync($"{basePath}/add-cart/", model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CartViewModel>();
            else throw new Exception("Something went wrong calling API");
        }
        public async Task<CartViewModel> UpdateCart(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync($"{basePath}/update-cart/", model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CartViewModel>();
            else throw new Exception("Something went wrong calling API");
        }
		public async Task<bool> UpdateQuantity(CartDetailViewModel model, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PutAsJsonAsync($"{basePath}/update-quantity/", model);
			if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
			else throw new Exception("Something went wrong calling API");
		}
		public async Task<bool> RemoveFromCart(int cartId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{basePath}/remove-cart/{cartId}");
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong calling API");
        }
        public async Task<bool> ApplyCoupon(CartViewModel model,  string token)
        {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PostAsJsonAsync($"{basePath}/apply-coupon", model);
			if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
			else throw new Exception("Something went wrong calling API");
		}
        public async Task<bool> RemoveCoupon(string userId, string token)
        {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.DeleteAsync($"{basePath}/remove-coupon/{userId}");
			if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
			else throw new Exception("Something went wrong calling API");
		}
        public Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<object> Checkout(CartHeaderViewModel model, string token)
        {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PostAsJsonAsync($"{basePath}/checkout", model);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<CartHeaderViewModel>();
            }else if (response.StatusCode.ToString().Equals("PreconditionFailed"))
            {
                return "Sorry coupon discount has changed, please confirm";
            }
            else throw new Exception("Something went wrong calling API");
		}

    }
}
