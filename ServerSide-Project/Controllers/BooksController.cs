using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


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
                return View("ListBookDetails", Book.createBook(book));
            }
            else
                return RedirectToAction("BrowseAllBooks", "Books", null);
        }

        [HttpGet]
        public ActionResult BrowseAllBooks(int? page)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var bookList = Book.getAllBooks(pageIndex, ITEMS_PER_PAGE);
            return View("BrowseAllBooks", bookList);
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            var a = Book.getBookFromIsbn(id);
            return View("ListBookDetails", a);
        }

        [HttpGet]
        public ActionResult EditBook(string id)
        {
            return View("EditBook",Book.getBookFromIsbn(id)); // ret a book with ISBN equal to id
        }


        [HttpPost]
        public ActionResult UpdateBook(Book book)
        {

            if (ModelState.IsValid) //validate the data
            {
                //int a = 5;
            }
            
            string oldISBN = (string)TempData["ISBN"];

            return RedirectToAction("BrowseAllBooks", "Books");
        }

        [HttpPost]
        public ActionResult deleteBook(string id)
        {
            if (Book.deleteBook(id))
                return RedirectToAction("BrowseAllBooks", "Books", null);
            else
                return RedirectToAction("BrowseAllBooks", "Books", null);
        }

        [HttpGet]
        public ActionResult SearchBooks(string search ,int? page, params int[] classifications)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            return View("BrowseSearchedBooks", Book.SearchBooks(search, pageIndex, ITEMS_PER_PAGE, classifications));
        }
    }
}