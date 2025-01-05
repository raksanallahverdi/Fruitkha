using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int StockQuantity { get; set; }
		public string PhotoName { get; set; }
		public int Price { get; set; }
	
		public int ProductTypeId { get; set; }
		public ProductType ProductType { get; set; }
		public ICollection<BasketProduct> BasketProducts { get; set; }

	}
}
