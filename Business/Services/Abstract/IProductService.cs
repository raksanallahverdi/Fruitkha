using Business.Models.Product;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductCreateVM model);
        Task<ProductIndexVM> GetAllAsync();
        Task<ProductIndexVM> GetAllAsync(ProductIndexVM model);
        Task<ProductIndexVM> GetLastThreeAsync();
        Task<List<Product>> GetAllByTypeAsync(int typeId);
      
        Task<Product> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Product product);





    }
}
