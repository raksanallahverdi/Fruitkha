using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public interface IBaseRepository<T> where T:BaseEntity
    {
        Task<List<T>> GetAllAsync();
		Task<List<T>> GetLastThreeAsync();
		Task<T> GetAsync(int id);
        Task CreateAsync(T data);

        Task UpdateAsync(T data);
        Task DeleteAsync(T data);
    }
}
