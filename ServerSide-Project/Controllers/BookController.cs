using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace ServerSide_Project.Controllers
{
    public class BookController : ControllerExtension
    {
        public const int ITEMS_PER_PAGE = 10;
        private BookManager Manager { get; } = new BookManager();

        [HttpGet]
        public ActionResult BrowseAllBooks(int page = 1)
        {
            return View("BrowseAllBooks", Manager.GetAllBooks(page, ITEMS_PER_PAGE));
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            return View("ListBookDetails", Manager.GetBookFromIsbn(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookPost(string id)
        {
            AuthorizeAndRedirect();
            Manager.DeleteBook(id);
            return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult DeleteBook(string id)
        {
            AuthorizeAndRedirect();
            return View("DeleteBook", Manager.GetBookFromIsbn(id));
        }

        [HttpGet]
        public ActionResult SearchBooks(string search, int page = 1, params int[] classifications)
        {
            return View("BrowseSearchedBooks", Manager.SearchBooks(search, page, ITEMS_PER_PAGE, classifications));
        }
    }

    
}