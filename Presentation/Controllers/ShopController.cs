using Business.Models.Product;
using Business.Services.Abstract;
using Common.Entities;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class ShopController : Controller
	{
		private readonly IProductService _productService;
		private readonly IProductRepository _productRepository;

		public ShopController(IProductService productService,
			IProductRepository productRepository)
		{
			_productService = productService;
			_productRepository = productRepository;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var model = await _productService.GetAllAsync();
			return View(model);
		}
		[HttpGet]


		[HttpGet]
		public async Task<IActionResult> FilterByType(int typeId)
		{
			

			var filteredProducts = await _productService.GetAllByTypeAsync(typeId);
		

			var model = new ProductIndexVM
			{
				Products = filteredProducts
			};

			return PartialView("_ProductPartial", model);
		}






	}
}
