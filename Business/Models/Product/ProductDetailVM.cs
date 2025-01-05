using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Product
{
	public class ProductDetailVM
	{

		public string Name { get; set; }
		public string Description { get; set; }
		public int StockQuantity { get; set; }
		public string PhotoName { get; set; }
		public int Price { get; set; }

		public int ProductTypeId { get; set; }
		public List<SelectListItem>? ProductTypes { get; set; }
	}
}
