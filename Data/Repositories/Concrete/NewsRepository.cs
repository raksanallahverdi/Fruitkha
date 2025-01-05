﻿using Common.Entities;
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
    public class NewsRepository:BaseRepository<News>,INewsRepository
    {
        private readonly AppDbContext _context;

        public NewsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
		public async Task<News> GetAsync(int id)
		{
			return await _context.AllNews.Include(news => news.Comments).ThenInclude(comment => comment.User).FirstOrDefaultAsync(news => news.Id == id);
		}
     

    }
}
