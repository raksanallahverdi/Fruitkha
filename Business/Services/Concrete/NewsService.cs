using Business.Models.Comment;
using Business.Models.ContactMessage;
using Business.Models.News;
using Business.Models.Product;
using Business.Services.Abstract;
using Business.Utilities.EmailHandler.Models;
using Common.Entities;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class NewsService:INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsRepository _newsRepository;
        private readonly ModelStateDictionary _modelState;

        public NewsService(IUnitOfWork unitOfWork,
            INewsRepository newsRepository,
            IActionContextAccessor actionContextAccessor
            )
        {
            _unitOfWork = unitOfWork;
            _newsRepository = newsRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<NewsIndexVM> GetAllAsync()
        {

            return new NewsIndexVM
            {
                AllNews = await _newsRepository.GetAllAsync()
            };

		}
      
        public async Task<NewsIndexVM> GetLastThreeAsync()
		{
            return new NewsIndexVM
            {
                AllNews = await _newsRepository.GetLastThreeAsync()
            };
		}
		public async Task<NewsDetailVM> GetAsync(int NewsId)
        {
			var news = await _newsRepository.GetAsync(NewsId);
			if (news == null)
			{
				throw new KeyNotFoundException($"News item with ID {NewsId} not found.");
			}
			var model = new NewsDetailVM
			{
				Title = news.Title,
				Description = news.Description,
				PhotoName = news.PhotoName,
				Tags = news.Tags,
                CreatedDate = news.CreatedDate
			};
			model.Comments = news.Comments.Select(c => new CommentVM
			{
				PhotoName = c.PhotoName,
				Description = c.Description,
                Email=c.User.Email,
				CreatedDate = c.CreatedDate
			}).ToList();

			return model;

		}


	}
}
