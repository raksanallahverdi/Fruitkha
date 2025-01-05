using Business.Models.News;
using Business.Models.TeamMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
	public interface ITeamMemberService
	{
		Task<TeamMemberIndexVM> GetAllAsync();
	}
}
