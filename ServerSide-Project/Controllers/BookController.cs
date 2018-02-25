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
            ValidateAndRedirect(Rank.Admin);
            return View("CreateBook");
        }

        [HttpPost]             
        public ActionResult CreateBook(Book book) 
        {
            ValidateAndRedirect(Rank.Admin);
            if (ModelState.IsValid)
            {
                return View("ListBookDetails", BookManager.CreateBook(book));
            }
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult BrowseAllBooks(int page = 1)
        {
            return View("BrowseAllBooks", BookManager.GetAllBooks(page, ITEMS_PER_PAGE));
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            return View("ListBookDetails", BookManager.GetBookFromIsbn(id));
        }

        [HttpGet]
        public ActionResult EditBook(string id)
        {
            ValidateAndRedirect(Rank.Admin);
            return View("EditBook", BookManager.GetBookFromIsbn(id));
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            ValidateAndRedirect(Rank.Admin);
            return RedirectToAction("ListBookDetails", "Book", BookManager.EditBook(book).ISBN);

        }

        [HttpPost]
        public ActionResult DeleteBook(string id)
        {
            ValidateAndRedirect(Rank.Admin);
            if (BookManager.DeleteBook(id))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult SearchBooks(string search, int page = 1, params int[] classifications)
        {
            return View("BrowseSearchedBooks", BookManager.SearchBooks(search, page, ITEMS_PER_PAGE, classifications));
        }
    }
}