using Microsoft.AspNetCore.Mvc.Rendering;
namespace Business.Models.Product
{
    public class ProductCreateVM
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
