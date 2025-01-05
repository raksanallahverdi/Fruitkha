using Business.Models.Basket;
using Business.Utilities.Stripe;
using Common.Entities;
using Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Identity.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly StripeSettings _stripeSettings;
        public CheckOutController(UserManager<User> userManager,
            AppDbContext context,
            IOptions<StripeSettings> stripeSettings)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.PublishableKey=_stripeSettings.PublishableKey;
            var authUser = _userManager.GetUserAsync(User).Result;
            if (authUser == null) return Unauthorized();

            var user = _userManager.Users.Include(x => x.Basket).FirstOrDefault(u => u.Id == authUser.Id);
            if (user?.Basket == null)
            {
                return View(new BasketIndexVM
                {
                    BasketProducts = new List<BasketProduct>()
                });
            }

            var basketProducts = _context.BasketProducts
                .Include(bp => bp.Product)
                .Where(bp => bp.BasketId == user.Basket.Id)
                .ToList();

            var model = new BasketIndexVM
            {
                BasketProducts = basketProducts
            };

            return View(model);
        }
        [HttpPost]
        [HttpPost]
        public IActionResult DeleteBasketProducts(int basketId)
        {
           
            var basket = _context.Baskets.FirstOrDefault(b => b.Id == basketId);

      
            if (basket != null)
            {
               
                var basketProducts = _context.BasketProducts.Where(bp => bp.BasketId == basketId).ToList();

                if (basketProducts.Any())
                {
                 
                    _context.BasketProducts.RemoveRange(basketProducts);
                    _context.SaveChanges();
                }

               
              
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}

