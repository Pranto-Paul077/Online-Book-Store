using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; // Include() ব্যবহার করার জন্য এটি বাধ্যতামূলক
using OnlineBookStore.Data;
using OnlineBookStore.Models;
using System.Linq;

namespace OnlineBookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // ১. কার্ট পেজ দেখানো
        // ==========================================
        public IActionResult Index()
        {
            // সেশন থেকে ইউজারের আইডি নেওয়া
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // ইউজারের কার্ট আইটেম খোঁজা এবং সাথে বইয়ের তথ্য (Title, Price, Image) যুক্ত করা
            var cartItems = _context.Carts
                .Include(c => c.Book) // এই লাইনের মাধ্যমে Book টেবিলের সাথে জয়েন করা হচ্ছে
                .Where(c => c.UserId == userId)
                .ToList();

            return View(cartItems);
        }

        // ==========================================
        // ২. কার্টে বই যুক্ত করা (Add to Cart)
        // ==========================================
        [HttpPost]
        public IActionResult Add(int bookId, int quantity)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // চেক করা যে ইউজারের কার্টে এই বইটি আগে থেকেই আছে কি না
            var existingCartItem = _context.Carts
                .FirstOrDefault(c => c.BookId == bookId && c.UserId == userId);

            if (existingCartItem != null)
            {
                // আগে থেকেই থাকলে শুধু পরিমাণ (Quantity) বাড়িয়ে দেওয়া
                existingCartItem.Quantity += quantity;
                _context.Carts.Update(existingCartItem);
            }
            else
            {
                // নতুন বই হলে কার্টে সেভ করা
                var newCartItem = new Cart
                {
                    UserId = (int)userId,
                    BookId = bookId,
                    Quantity = quantity
                    // Book অবজেক্ট নিজে থেকেই ডেটাবেস থেকে কানেক্ট হয়ে যাবে
                };

                _context.Carts.Add(newCartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ==========================================
        // ৩. কার্ট থেকে বই মুছে ফেলা
        // ==========================================
        public IActionResult Remove(int id)
        {
            var cartItem = _context.Carts.Find(id);

            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}