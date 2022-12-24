using GeekShopping.Web.Models;
using GeekShopping.Web.Models.ViewModel;
using GeekShopping.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindalAllProducts();
            return View(products);
        }
		public IActionResult ProductCreate()
		{
			return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> ProductCreate(ProductViewModel viewModel)
		{
			decimal price;
			decimal.TryParse(viewModel.Price, out price);
			if (ModelState.IsValid)
            {
				
				ProductModel model = new ProductModel()
				{
					Name = viewModel.Name,
					Price = price,
					CategoryName = viewModel.CategoryName,
					Description = viewModel.Description ?? "",
					ImageURL = viewModel.ImageURL,
				};
				var response = await _productService.CreateProduct(model);
                if(response != null) return RedirectToAction(nameof(ProductIndex));
            }
            return View(viewModel);
		}
		public async Task<IActionResult> ProductUpdate(int id)
		{
			var model = await _productService.FindByIdProduct(id);
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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ProductUpdate(ProductViewModel viewModel)
		{
			decimal price;
			decimal.TryParse(viewModel.Price, out price);
			if (ModelState.IsValid)
			{
				ProductModel model = new ProductModel()
				{
					Id = viewModel.Id,
					Name = viewModel.Name,
					Price = price,
					CategoryName = viewModel.CategoryName,
					Description = viewModel.Description ?? "",
					ImageURL = viewModel.ImageURL,
				};

				var response = await _productService.UpdateProduct(model);
				if (response != null) return RedirectToAction(nameof(ProductIndex));
			}
			return View(viewModel);
		}
		public async Task<IActionResult> ProductDelete(int id)
		{
			var model = await _productService.FindByIdProduct(id);
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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ProductDelete(ProductViewModel viewModel)
		{
			var response = await _productService.DeleteProductById(viewModel.Id);
			if (response) return RedirectToAction(nameof(ProductIndex));	
			return View(viewModel);
		}
	}
}
