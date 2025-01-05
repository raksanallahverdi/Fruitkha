using Business.Models.ContactMessage;
using Business.Models.News;
using Business.Models.Product;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface INewsService
    {
        Task<NewsIndexVM> GetAllAsync();
		Task<NewsIndexVM> GetLastThreeAsync();
        Task<NewsDetailVM> GetAsync(int NewsId);
       

    }
}
