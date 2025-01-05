using Business.Services.Abstract;
using Business.Services.Concrete;
using Data.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _teamMemberService.GetAllAsync();
            return View(model);
        }
       
    }
}
