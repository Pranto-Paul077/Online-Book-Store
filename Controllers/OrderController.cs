using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Data;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Checkout Page
        public IActionResult Checkout()
        {
            return View();
        }

        // Place Order
        [HttpPost]
        public IActionResult PlaceOrder(string name, string phone, string address)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = _context.Carts
                .Where(c => c.UserId == userId)
                .ToList();

            decimal total = 0;

            foreach (var item in cartItems)
            {
                var book = _context.Books.Find(item.BookId);
                total += book.Price * item.Quantity;
            }

            Order order = new Order()
            {
                UserId = userId.Value,
                TotalPrice = total,
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                var book = _context.Books.Find(item.BookId);

                OrderItem orderItem = new OrderItem()
                {
                    OrderId = order.OrderId,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = book.Price
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.SaveChanges();

            _context.Carts.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("Success");
        }

        // Order Success Page
        public IActionResult Success()
        {
            return View();
        }
    }
}