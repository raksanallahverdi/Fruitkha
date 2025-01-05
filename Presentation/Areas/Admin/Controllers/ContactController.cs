
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Business.Models.ContactMessage;
using Data.Contexts;

namespace Technify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        #region Index
        public IActionResult Index()
        {
            // Retrieve all messages from the database
            var messages = _context.ContactMessages
                .Select(m => new MessageVM
                {

                    Name = m.Name,
                    Email = m.Email,
                    Subject = m.Subject,
                    Message = m.Message
                })
                .ToList();

            var model = new MessageIndexVM
            {
                Messages = messages
            };

            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Find the message by its ID
            var message = _context.ContactMessages.FirstOrDefault(m => m.Id == id);
            if (message == null) return NotFound();

            // Remove the message from the database
            _context.ContactMessages.Remove(message);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
        #endregion