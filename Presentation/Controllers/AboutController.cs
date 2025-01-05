using Business.Models.Home;
using Business.Models.TeamMember;
using Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class AboutController : Controller
	{
		private readonly ITeamMemberService _teamMemberService;

		public AboutController(ITeamMemberService teamMemberService,
			INewsService newsService)
		{
			_teamMemberService = teamMemberService;
			
		}
		public async Task<IActionResult> Index()
		{
			var listMembers =await  _teamMemberService.GetAllAsync();
			var model = new TeamMemberIndexVM
			{
				TeamMembers=listMembers.TeamMembers
			};
			return View(model);
		}


	}
}
