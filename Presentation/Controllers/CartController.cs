using Business.Models.Basket;
using Business.Utilities.Stripe;
using Common.Entities;
using Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
namespace Identity.Controllers
{
    [Authorize]
    public class CartController:Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly StripeSettings _stripeSettings;

        public CartController(UserManager<User> userManager,
            AppDbContext context,
            IOptions<StripeSettings> stripeSettings)
        {
            _context = context;
            _stripeSettings = stripeSettings.Value;
            _userManager =userManager;
        }
        public IActionResult Index()
        {
            ViewBag.PublishableKey=_stripeSettings.PublishableKey;
            var authUser = _userManager.GetUserAsync(User).Result;
            if (authUser == null) return Unauthorized();
            var user=_userManager.Users.Include(x=>x.Basket).FirstOrDefault(u=>u.Id==authUser.Id);
			if (user?.Basket == null)
			{
				
				return View(new BasketIndexVM
				{
					BasketProducts = new List<BasketProduct>() 
				});
			}
			var model = new BasketIndexVM
            {
                BasketProducts = _context.BasketProducts.Include(bp=>bp.Product).Where(bp => bp.BasketId == user.Basket.Id).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddProduct(int productId)
        {
            var user=_userManager.GetUserAsync(User).Result;
            if(user is null) return Unauthorized("Couldn't add product to basket");
            var product=_context.Products.Find(productId);
            if(product is null) return NotFound("Couldn't add product to basket");
            if (product.StockQuantity == 0)
            {
                return BadRequest("Product out of stock");
            }
			var basket = _context.Baskets.Include(b => b.BasketProducts).ThenInclude(bp => bp.Product).FirstOrDefault(b => b.UserId == user.Id);
			if (basket is null){
                basket=new Basket
                {
                    UserId = user.Id,
					CreatedDate = DateTime.Now,
                    BasketProducts = new List<BasketProduct>()
                };
                _context.Baskets.Add(basket);
            }
            var existProduct = basket.BasketProducts.FirstOrDefault(bp => bp.ProductId == productId);
			if (existProduct is null)
            {
                var basketProduct = new BasketProduct
                {
                    Basket = basket,
                    Quantity = 1,
                    ProductId = product.Id,
                    CreatedDate = DateTime.Now,
                    TotalPrice=product.Price * 1
                };
                basket.BasketProducts.Add(basketProduct);
                _context.BasketProducts.Add(basketProduct);
            }
            else
            {
                existProduct.Quantity += 1;
				existProduct.TotalPrice = existProduct.Product.Price * existProduct.Quantity;
				_context.BasketProducts.Update(existProduct);
            }
            _context.SaveChanges();
            return Ok("Product Added to Basket Successfully");

        }
        public IActionResult RemoveProduct(int productId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user is null) return Unauthorized("User not found");

            var basket = _context.Baskets.Include(b => b.BasketProducts)
                .FirstOrDefault(b => b.UserId == user.Id);

            if (basket == null)
            {
                return BadRequest("No basket found for this user");
            }

          
            var basketProduct = basket.BasketProducts.FirstOrDefault(bp => bp.Id == productId);

            if (basketProduct == null)
            {
                return NotFound("Product not found in the basket");
            }

           
            _context.BasketProducts.Remove(basketProduct);

          
            _context.SaveChanges();

            return Ok("Product removed from the basket successfully");
        }
        [HttpPost]
  
        public IActionResult UpdateCart(Dictionary<int, int> Quantities)
        {
            if (Quantities == null || !Quantities.Any())
            {
                return BadRequest("No quantities were provided.");
            }

            var user = _userManager.GetUserAsync(User).Result;
            if (user == null) return Unauthorized();

            foreach (var item in Quantities)
            {
                var productId = item.Key;
                var quantity = item.Value;

                var basketProduct = _context.BasketProducts.Include(bp => bp.Product).FirstOrDefault(bp => bp.Id == productId);


                if (basketProduct != null)
                {
                    basketProduct.Quantity = quantity;
                    basketProduct.TotalPrice = quantity * basketProduct.Product.Price;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }







    }
}
