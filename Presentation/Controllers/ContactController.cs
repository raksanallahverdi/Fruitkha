using Business.Models.ContactMessage;
using Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IMessageService _messageService;

        public ContactController(IMessageService messageService)
        {
			_messageService = messageService;
        }
        public IActionResult Index()
        { 
        return View();
        }

            [HttpPost]
        public IActionResult SendMessage(MessageVM messageVM)
        {
            if (ModelState.IsValid)
            {
             
                var result = _messageService.SendMessage(messageVM);

                if (result)
                {
                    TempData["SuccessMessage"] = "Your message has been sent successfully!";
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["ErrorMessage"] = "There was an error. Please correct the highlighted fields.";
            return View("Index", messageVM); // Return the same view with validation messages
        }
    }

}
