using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Data;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        // Book List
        public IActionResult BookList()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        // Add Book Page
        public IActionResult AddBook()
        {
            return View();
        }

        // Add Book
        [HttpPost]
        public IActionResult AddBook(Book book, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string folder = Path.Combine(_environment.WebRootPath, "images/books");
                string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                book.ImagePath = "/images/books/" + fileName;
            }

            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("BookList");
        }

        // Edit Book Page
        public IActionResult EditBook(int id)
        {
            var book = _context.Books.Find(id);
            return View(book);
        }

        // Update Book
        [HttpPost]
        public IActionResult EditBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();

            return RedirectToAction("BookList");
        }

        // Delete Book
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }

            return RedirectToAction("BookList");
        }
    }
}