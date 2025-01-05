﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class Comment:BaseEntity
	{
		public string PhotoName { get; set; }
		public string Description { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		
		public int NewsId { get; set; }
		public News News { get; set; }

	}
}
