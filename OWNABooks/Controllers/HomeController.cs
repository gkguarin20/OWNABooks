using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OWNABooks.Models;
using System.Diagnostics;

namespace OWNABooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static IEnumerable<Book> _books;
        private static IEnumerable<Author> _authors;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;


            GetLibrary();
        }

        public IActionResult Index()
        {
            return View("Search", new { query = string.Empty} );
        }

        public IActionResult Search(string query, int page = 1, int pageSize = 10)
        {
            if (!string.IsNullOrEmpty(query))
            {
                _books = _books.Where(b => b.Title.Contains(query) || b.Authors.Any(a => a.Name.Contains(query)));
            }

            var totalBooks = _books.Count();
            var totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);

            var booksToDisplay = _books.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(booksToDisplay);
        }

        public IActionResult Details(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            return View(book);
        }

        public IActionResult Create()
        {
            ViewBag.Authors = _authors;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                int newBookId = _books.Count() + 1;
                book.Id = newBookId;

                _books = _books.Concat(new[] { book });

                return RedirectToAction("Details", new { id = book.Id });
            }

            ViewBag.Authors = _authors;
            return View(book);
        }

        public IActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                int newAuthorId = _authors.Count() + 1;
                author.Id = newAuthorId;

                _authors = _authors.Concat(new[] { author });

                return RedirectToAction("Search");
            }

            return View(author);
        }

        private static void GetLibrary()
        {
            string json = System.IO.File.ReadAllText("App_Data\\Books.json");

            var library = JsonConvert.DeserializeObject<Library>(json);

            if (_books == null)
                _books = library?.Books;

            if (_authors == null)
                _authors = library?.Authors;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}