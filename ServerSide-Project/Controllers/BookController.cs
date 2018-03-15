using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
            ValidateAndRedirect();
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

        [HttpGet]
        public string SEARCH(string search)
        {
            var a = new BookManager().SearchBooks(new Search() { SearchQuery = search, SelectedClassifications = new List<int>() { 1,2,3,4,5 } }, 1, 20);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(a);
            return json;
        }
    }
}