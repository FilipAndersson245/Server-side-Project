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
        [ActionName("CreateBook")]              // cannot have equal names so using this to emulate same same
        public ActionResult CreateBookPost(Book book)    //
        {
            if (ModelState.IsValid)
            {
                //valid book!
            }
            //book.Authors = new Author { ID = "11", FirstName = "Test", LastName = "Author", BirthYear = 2000 };
            book.BookClassification = new Classification { Signum = "TestGenre" };
            return RedirectToAction("ListBooks", "Books", null); //maybe to the created book instead of list
        }

        [HttpGet]
        public ActionResult ListBooks(int? page)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var bookList = Book.getAllBooks(pageIndex, ITEMS_PER_PAGE);
            return View("ListBooks", bookList);
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            return View("ListBookDetails");
        }

        [HttpGet]
        public ActionResult EditBook(string id)
        {
            return View("EditBook"); // ret a book with ISBN equal to id
        }


        [HttpPost]
        public ActionResult UpdateBook(Book book)
        {

            if (ModelState.IsValid) //validate the data
            {
                //int a = 5;
            }
            
            string oldISBN = (string)TempData["ISBN"];

            return RedirectToAction("ListBooks", "Books");
        }

        [HttpGet]
        public ActionResult DeleteBook(string id)
        {
            return RedirectToAction("ListBooks", "Books", null);
        }

        [HttpPost]
        public ActionResult SearchBooks(string search)
        {
            return View("ListBooks", Book.SearchBooks(search,1,2));
        }
    }
}