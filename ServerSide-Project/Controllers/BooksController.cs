using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Service.Models;
using Service.Managers;


namespace ServerSide_Project.Controllers
{
    public class BooksController : Controller
    {
        public const int ITEMS_PER_PAGE = 10;

        [HttpGet]
        public ActionResult CreateBook()
        {
            return View("CreateBook");
        }

        [HttpPost]
        [ActionName("CreateBook")]              
        public ActionResult CreateBookPost(Book book) 
        {
            if (ModelState.IsValid)
            {
                return View("ListBookDetails", BookManager.createBook(book));
            }
            else
                return RedirectToAction("BrowseAllBooks", "Books", null);
        }

        [HttpGet]
        public ActionResult BrowseAllBooks(int? page)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var bookList = BookManager.getAllBooks(pageIndex, ITEMS_PER_PAGE);
            return View("BrowseAllBooks", bookList);
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            var a = BookManager.getBookFromIsbn(id);
            return View("ListBookDetails", a);
        }

        [HttpGet]
        public ActionResult EditBook(string id)
        {
            return View("EditBook", BookManager.getBookFromIsbn(id));
        }

        [HttpPost]
        [ActionName("EditBook")]
        public ActionResult EditBookPost(Book book)
        {
            //if (ModelState.IsValid) //validate the data
            //{
                return RedirectToAction("ListBookDetails", "Books", BookManager.editBook(book).ISBN);
            //}
            //else
            //    return RedirectToAction("BrowseAllBooks", "Books");
        }

        [HttpPost]
        public ActionResult deleteBook(string id)
        {
            if (BookManager.deleteBook(id))
                return RedirectToAction("BrowseAllBooks", "Books", null);
            else
                return RedirectToAction("BrowseAllBooks", "Books", null);
        }

        [HttpGet]
        public ActionResult SearchBooks(string search ,int? page, params int[] classifications)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            return View("BrowseSearchedBooks", BookManager.SearchBooks(search, pageIndex, ITEMS_PER_PAGE, classifications));
        }
    }
}