using ServerSide_Project.Tools;
using Service.Managers;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class BookController : ControllerExtension
    {
        public const int ITEMS_PER_PAGE = 10;

        [HttpGet]
        public ActionResult BrowseAllBooks(int page = 1)
        {
            BookManager bookManager = new BookManager();
            return View("BrowseAllBooks", bookManager.GetAllBooks(page, ITEMS_PER_PAGE));
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            BookManager bookManager = new BookManager();
            return View("ListBookDetails", bookManager.GetBookFromIsbn(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookPost(string id)
        {
            AuthorizeAndRedirect();
            BookManager bookManager = new BookManager();
            if (bookManager.DeleteBook(id))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult DeleteBook(string id)
        {
            BookManager manager = new BookManager();
            return View("DeleteBook", manager.GetBookFromIsbn(id));
        }

        [HttpGet]
        public ActionResult SearchBooks(string search, int page = 1, params int[] classifications)
        {
            BookManager bookManager = new BookManager();
            return View("BrowseSearchedBooks", bookManager.SearchBooks(search, page, ITEMS_PER_PAGE, classifications));
        }

    }
}