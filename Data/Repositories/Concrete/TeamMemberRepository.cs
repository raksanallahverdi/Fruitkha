using Common.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
	public class TeamMemberRepository: BaseRepository<TeamMember>, ITeamMemberRepository
	{
		private readonly AppDbContext _context;

		public TeamMemberRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public Task CreateAsync(TeamMember data)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(TeamMember data)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(TeamMember data)
		{
			throw new NotImplementedException();
		}

	
	}
}
