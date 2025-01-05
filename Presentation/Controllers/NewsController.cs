using Business.Models.News;
using Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class NewsController : Controller
	{
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _newsService.GetAllAsync();
            return View(model);
        }
        public async Task<IActionResult> Single(int NewsId)
        {
           var model=await _newsService.GetAsync(NewsId);
            return View(model);
            
           

        }
    }
}
