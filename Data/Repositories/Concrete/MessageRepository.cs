using Common.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
	public class MessageRepository: BaseRepository<ContactMessage>, IMessageRepository
	{
		private readonly AppDbContext _context;

		public MessageRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
		public void SendMessage(ContactMessage message)
		{
			_context.ContactMessages.Add(message);
			_context.SaveChanges();
		}
	}
}
