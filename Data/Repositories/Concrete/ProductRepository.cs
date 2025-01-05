using Common.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class ProductRepository:BaseRepository<Product>,IProductRepository
    {
		private readonly AppDbContext _context;

		public ProductRepository(AppDbContext context):base(context) 
        {
			_context = context;
		}
        public IQueryable<Product> FilterByName(string name)
        {
			return name != null ? _context.Products.Include(p => p.ProductType).Where(p => p.Name.Contains(name)) : _context.Products.Include(p => p.ProductType);
        }

        public async Task<Product> GetByNameAsync(string name)
		{
			return await _context.Products.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
		}
		public async Task<List<Product>> GetAllByTypeAsync(int typeId)
		{
			return await _context.Products
				.Where(p => p.ProductTypeId == typeId)
				.ToListAsync();
		}
		public async Task<List<ProductType>> GetAllTypesAsync()
		{
			return await _context.ProductTypes.ToListAsync();
		}
       
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id);
        }
       
    }
}
