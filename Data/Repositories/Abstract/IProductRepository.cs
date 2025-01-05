﻿using Common.Entities;
using Data.Repositories.Base;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByNameAsync(string name);
        Task<List<ProductType>> GetAllTypesAsync();
        Task<List<Product>> GetAllByTypeAsync(int typeId);
        Task<Product> GetByIdAsync(int id);
        IQueryable<Product> FilterByName(string name);


    }
}
