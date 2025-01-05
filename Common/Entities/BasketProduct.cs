using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class BasketProduct:BaseEntity
	{
		public int BasketId { get; set; }
		public Basket Basket { get; set; }
		public Product Product { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public int TotalPrice { get; set; }

	}
}
