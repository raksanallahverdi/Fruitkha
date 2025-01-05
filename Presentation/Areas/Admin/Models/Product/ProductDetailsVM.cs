﻿namespace Presentation.Areas.Admin.Models.Product
{
  
        public class ProductDetailsVM
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public string ProductType { get; set; }
        }
    }


