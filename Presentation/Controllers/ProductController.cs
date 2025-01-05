using Business.Models.Product;
using Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
		[HttpGet]
        public async Task< IActionResult> Index()
		{
			var model=await _productService.GetAllAsync();
			return View(model);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(ProductCreateVM model)
		{
			var isSucceeded = await _productService.CreateAsync(model);
			if (isSucceeded) return RedirectToAction(nameof(Index));
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Single(int id)
		{
			var product = await _productService.GetByIdAsync(id);

			if (product == null)
			{
				return NotFound(); 
			}

			
			var productDetailVM = new ProductDetailVM
			{
				
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				PhotoName = product.PhotoName,
				StockQuantity = product.StockQuantity,
				ProductTypeId = product.ProductType.Id 
			};

			return View(productDetailVM);
		}
	
}
}
