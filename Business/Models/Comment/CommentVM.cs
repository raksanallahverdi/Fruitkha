using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Comment
{
	public class CommentVM
	{
		public string PhotoName { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
