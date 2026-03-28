using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Data;

namespace OnlineBookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show all books
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        // Book Details
        public IActionResult Details(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            return View(book);
        }
    }
}