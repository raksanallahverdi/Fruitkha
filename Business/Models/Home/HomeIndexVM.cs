using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Home
{
	public class HomeIndexVM
	{
		public List<Common.Entities.News> AllNews { get; set; }
		public List<Common.Entities.Product> Products { get; set; }
	}
}
