using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class News : BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string PhotoName { get; set; }
		public List<string> Tags { get; set; }
		public List<Comment> Comments { get; set; }
	}
}
