using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
	public interface IMessageRepository
	{
		public void SendMessage(ContactMessage message)
		{
			throw new NotImplementedException();
		}
	}
}
