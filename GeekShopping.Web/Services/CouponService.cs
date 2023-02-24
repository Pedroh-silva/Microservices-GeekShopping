using GeekShopping.Web.Models;
using System.Net.Http.Headers;
using GeekShopping.Web.Models.ViewModel;
using GeekShopping.Web.Services.IService;
using GeekShopping.Web.Utils;
using System.Net;

namespace GeekShopping.Web.Services
{
	public class CouponService : ICouponService
	{
		private readonly HttpClient _client;
		private const string basePath = "api/v1/coupon";

		public CouponService(HttpClient client)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
		}
		public async Task<CouponViewModel> GetCoupon(string couponCode, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.GetAsync($"{basePath}/{couponCode}");
			if(response.StatusCode != HttpStatusCode.OK) return new CouponViewModel();
			return await response.ReadContentAs<CouponViewModel>();
		}
	}
}
