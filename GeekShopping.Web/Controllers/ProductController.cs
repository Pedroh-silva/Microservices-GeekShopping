using GeekShopping.Web.Models;
using GeekShopping.Web.Models.ViewModel;
using GeekShopping.Web.Services.IService;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
		[Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts("");
            return View(products);
        }
		public IActionResult ProductCreate()
		{
			return View();
		}
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> ProductCreate(ProductViewModel viewModel)
		{
			decimal price;
			decimal.TryParse(viewModel.Price, out price);
			if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                ProductModel model = new ProductModel()
				{
					Name = viewModel.Name,
					Price = price,
					CategoryName = viewModel.CategoryName,
					Description = viewModel.Description ?? "",
					ImageURL = viewModel.ImageURL,
				};
				var response = await _productService.CreateProduct(model, token);
                if(response != null) return RedirectToAction(nameof(ProductIndex));
            }
            return View(viewModel);
		}
		public async Task<IActionResult> ProductUpdate(int id)
		{
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindByIdProduct(id, token);
			if (model != null)
			{
				ProductViewModel viewModel = new ProductViewModel()
				{
					Id = model.Id,
					Name = model.Name,
					CategoryName = model.CategoryName,
					Description = model.Description,
					ImageURL = model.ImageURL,
					Price = model.Price.ToString()
				};
				return View(viewModel);
			}
			return NotFound();
		}
        [Authorize]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ProductUpdate(ProductViewModel viewModel)
		{
			decimal price;
			decimal.TryParse(viewModel.Price, out price);
			if (ModelState.IsValid)
			{

                var token = await HttpContext.GetTokenAsync("access_token");
                ProductModel model = new ProductModel()
				{
					Id = viewModel.Id,
					Name = viewModel.Name,
					Price = price,
					CategoryName = viewModel.CategoryName,
					Description = viewModel.Description ?? "",
					ImageURL = viewModel.ImageURL,
				};

				var response = await _productService.UpdateProduct(model, token);
				if (response != null) return RedirectToAction(nameof(ProductIndex));
			}
			return View(viewModel);
		}
        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
		{
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindByIdProduct(id, token);
			if (model != null)
			{
                ProductViewModel viewModel = new ProductViewModel()
				{
					Id = model.Id,
					Name = model.Name,
					CategoryName = model.CategoryName,
					Description = model.Description,
					ImageURL = model.ImageURL,
					Price = model.Price.ToString()
				};
				return View(viewModel);
			}
			return NotFound();
		}
        
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> ProductDelete(ProductViewModel viewModel)
		{
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductById(viewModel.Id, token);
			if (response) return RedirectToAction(nameof(ProductIndex));	
			return View(viewModel);
		}
	}
}
