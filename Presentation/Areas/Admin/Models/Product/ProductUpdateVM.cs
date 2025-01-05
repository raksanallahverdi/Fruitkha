using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Areas.Admin.Models.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoName { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }
        public int ProductTypeId { get; set; }
        public List<SelectListItem>? ProductTypes { get; set; }
    }
}
