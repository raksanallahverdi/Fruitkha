namespace Business.Models.Product
{
	public class ProductIndexVM
	{
		public string? Name { get; set; }
		public List<Common.Entities.Product> Products { get; set; }
		public List<Common.Entities.ProductType> ProductTypes { get; set; }
	}
}
