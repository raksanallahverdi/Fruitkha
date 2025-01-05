using Business.Models.ContactMessage;
using Business.Models.News;
using Business.Services.Abstract;
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
	public class MessageService:IMessageService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMessageRepository _messageRepository;
		private readonly ModelStateDictionary _modelState;

		public MessageService(IUnitOfWork unitOfWork,
			IMessageRepository messageRepository,
			IActionContextAccessor actionContextAccessor
			)
		{
			_unitOfWork = unitOfWork;
			_messageRepository = messageRepository;
			_modelState = actionContextAccessor.ActionContext.ModelState;
		}
	
		public bool SendMessage(MessageVM messageVM)
		{

			var contactMessage = new ContactMessage
			{
				Name = messageVM.Name,
				Email = messageVM.Email,
				Subject = messageVM.Subject,
				Message = messageVM.Message,
				PhoneNumber = messageVM.PhoneNumber,
				CreatedDate= DateTime.Now
			};


			_messageRepository.SendMessage(contactMessage);


			return true;
		}
	}
}
