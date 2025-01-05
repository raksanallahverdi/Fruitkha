using Common.Entities;
using Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface INewsRepository : IBaseRepository<News>
    {
        public Task CreateAsync(News data)
        {
            throw new NotImplementedException();
        }
        public void SendMessage(ContactMessage message)
        {
            throw new NotImplementedException();
        }


        public Task DeleteAsync(News data)
        {
            throw new NotImplementedException();
        }

        public Task<List<News>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<News> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<News>> GetLastThreeAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(News data)
        {
            throw new NotImplementedException();
        }
    }
}
