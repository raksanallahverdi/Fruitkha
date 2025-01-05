using Business.Services.Abstract;
using Business.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using Business.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Business.Models.Home;

namespace Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
		private readonly IProductService _productService;
        private readonly INewsService _newsService;
		public HomeController(IProductService productService,
            INewsService newsService)
        {
			_productService = productService;
            _newsService = newsService;
		}
		
        [HttpGet]
        public async Task<IActionResult> Index()
        {
			var lastThreeProducts = await _productService.GetLastThreeAsync();
			var lastThreeNews = await _newsService.GetLastThreeAsync();

			if (lastThreeProducts == null || lastThreeNews == null)
			{
				return Content("No data available.");
			}

			var model = new HomeIndexVM
			{
				Products = lastThreeProducts.Products,
				AllNews = lastThreeNews.AllNews
			};
			return View(model);
		}


    }
}
