using Business.Models.News;
using Business.Models.TeamMember;
using Business.Services.Abstract;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
	public class TeamMemberService:ITeamMemberService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITeamMemberRepository _teamMemberRepository;
		private readonly ModelStateDictionary _modelState;

		public TeamMemberService(IUnitOfWork unitOfWork,
			ITeamMemberRepository teamMemberRepository,
			IActionContextAccessor actionContextAccessor
			)
		{
			_unitOfWork = unitOfWork;
			_teamMemberRepository = teamMemberRepository;
			_modelState = actionContextAccessor.ActionContext.ModelState;
		}
		public async Task<TeamMemberIndexVM> GetAllAsync()
		{

			return new TeamMemberIndexVM
			{
				TeamMembers = await _teamMemberRepository.GetAllAsync()

			};

		}
	}
}
