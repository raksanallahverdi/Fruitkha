using Business.Models.Comment;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.News
{
	public class NewsDetailVM
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string PhotoName { get; set; }
		public List<string> Tags { get; set; }
		public List<CommentVM> Comments { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
