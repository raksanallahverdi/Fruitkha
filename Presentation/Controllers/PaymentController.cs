using Common.Constants;
using Common.Entities;
using Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using Stripe;
namespace Presentation.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public PaymentController(UserManager<User> userManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Pay()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null) return Unauthorized();
            user=_context.Users.Include(u=>u.Basket).ThenInclude(b=>b.BasketProducts).ThenInclude(bp=>bp.Product).FirstOrDefault(u=>u.Id==user.Id);
            var order = new Order
            {
                Status = OrderStatus.Pending,
                CreatedDate = DateTime.Now,
                UserId = user.Id,
                PaymentToken=Guid.NewGuid()

            };
            _context.Orders.Add(order);
          

            var items = new List<SessionLineItemOptions>();
            foreach (var basketProduct in user.Basket.BasketProducts)
            {
               
                var orderProduct = new OrderProduct
                {
                    Order = order,
                    Price=basketProduct.Product.Price,
                    Quantity=basketProduct.Quantity,
                    Product=basketProduct.Product

                };
                _context.OrderProducts.Add(orderProduct);
                var item = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = orderProduct.Price * 100,
                        Currency = "AZN",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = orderProduct.Product.Name,
                        }
                    },
                    Quantity=orderProduct.Quantity
                };
                items.Add(item);
            }
            _context.SaveChanges();
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                LineItems= items,
                SuccessUrl = Url.Action("Success", "Payment", new { token = order.PaymentToken }, Request.Scheme),
                CancelUrl = Url.Action("Cancel", "Payment", new { token = order.PaymentToken }, Request.Scheme),
            };
            try
            {
                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                return Json(new { id = session.Id });
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.Message });
            }
           
         
        }
        public IActionResult Success( Guid token)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null) return Unauthorized();
            user = _context.Users.Include(u => u.Basket).FirstOrDefault(u => u.Id == user.Id);
            var order=_context.Orders.Include(o=>o.OrderProducts).FirstOrDefault(o=>o.PaymentToken == token &&
                                                        o.Status==OrderStatus.Pending &&
                                                        o.UserId==user.Id
                                                        );
            if (order == null) return NotFound();
            order.Status = OrderStatus.Success;
            foreach(var orderProduct in order.OrderProducts)
            {
                var product = _context.Products.Find(orderProduct.ProductId);
                if(product is not null)
                {
                    product.StockQuantity -= orderProduct.Quantity;
                }
                    _context.Products.Update(product);
            }
            _context.Baskets.Remove(user.Basket);
            _context.SaveChanges();
          
            return View();
        }
        public IActionResult Cancel(Guid token)
        {
            var user=_userManager.GetUserAsync(User).Result;
            if (user == null) return Unauthorized();
            var order = _context.Orders.Include(o => o.OrderProducts).FirstOrDefault(o => o.PaymentToken == token &&
                                                       o.Status == OrderStatus.Pending &&
                                                       o.UserId == user.Id
                                                       );
            if (order == null) return View();
            order.Status = OrderStatus.Failed;
            _context.Orders.Update(order);
            _context.SaveChanges();
            return View();
        }
    }
}
