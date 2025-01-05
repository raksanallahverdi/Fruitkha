using Common.Entities;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public BaseRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _table =context.Set<T>();
            
        }
        public async Task<List<T>> GetAllAsync()
        {
           return await _table.ToListAsync();   
        }
		public async Task<List<T>> GetLastThreeAsync()
		{
			return await _table
				.OrderByDescending(entity => entity.CreatedDate) 
				.Take(3)
				.ToListAsync();
		}


		public async Task<T> GetAsync(int id)
        {
            return await _table.FindAsync(id);
        }
        public async Task CreateAsync(T data)
        {
            await _table.AddAsync(data);
        }
        public async Task UpdateAsync(T data)
        {
            _table.Update(data);
            await _context.SaveChangesAsync(); 
        }
        public async Task DeleteAsync(T data)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("The database context is not initialized.");
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "The entity to delete cannot be null.");
            }
            _table.Remove(data);
            await _context.SaveChangesAsync();

        }


    }
}
