using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Service.Models;
using Service.Managers;
using ServerSide_Project.Tools;

namespace ServerSide_Project.Controllers
{
    public class BookController : ControllerExtension
    {
        public const int ITEMS_PER_PAGE = 10;

        [HttpGet]
        public ActionResult CreateBook()
        {
            ValidateAndRedirect();
            return View("CreateBook");
        }

        [HttpPost]             
        public ActionResult CreateBook(Book book) 
        {
            ValidateAndRedirect();
            BookManager bookManager = new BookManager();
            if (ModelState.IsValid)
            {
                return View("ListBookDetails", bookManager.CreateBook(book));
            }
            else
            {
                ModelState.AddModelError("", new Exception("aaa"));
                return RedirectToAction("BrowseAllBooks", "Book", null);
            }
                
        }

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

        [HttpGet]
        public ActionResult EditBook(string id)
        {
            ValidateAndRedirect();
            BookManager bookManager = new BookManager();
            return View("EditBook", bookManager.GetBookFromIsbn(id));
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            ValidateAndRedirect();
            BookManager bookManager = new BookManager();
            return RedirectToAction("ListBookDetails", "Book", bookManager.EditBook(book).ISBN);

        }

        [HttpPost]
        public ActionResult DeleteBook(string id)
        {
            ValidateAndRedirect();
            BookManager bookManager = new BookManager();
            if (bookManager.DeleteBook(id))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult SearchBooks(string search, int page = 1, params int[] classifications)
        {
            BookManager bookManager = new BookManager();
            return View("BrowseSearchedBooks", bookManager.SearchBooks(search, page, ITEMS_PER_PAGE, classifications));
        }
    }
}