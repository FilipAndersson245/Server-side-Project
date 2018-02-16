using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

namespace ServerSide_Project.Controllers
{
    public class AuthorsController : Controller
    {
        public const int ITEMS_PER_PAGE = 15;
        public const int ITEMS_PER_AUTHOR_PAGE = 5;

        // GET: Authors
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAuthor()
        {
            return View("CreateAuthor");
        }

        [HttpPost]
        public ActionResult CreateAuthor(Author author)
        {
            return RedirectToAction("ListAuthors", "Authors", null);
        }

        
        public ActionResult DeleteAuthor(string id)
        {
            return RedirectToAction("ListAuthors", "Authors", null);
        }

        [HttpGet]
        public ActionResult ListAuthors(int? page)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var authorList = Author.getAllAuthors(pageIndex, ITEMS_PER_PAGE);
            return View("ListAuthors", authorList);
        }

        [HttpGet]
        public ActionResult ListAuthorDetails(int id, int? bookPage)
        {
            int bookPageIndex = bookPage.HasValue ? Convert.ToInt32(bookPage) : 1;
            Author author = Author.getAuthorDetails(id, bookPageIndex, ITEMS_PER_AUTHOR_PAGE);
            return View("ListAuthorDetails", author);
        }

        [HttpGet]
        public ActionResult searchAuthors(string search, int? page)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            return View("ListAuthors", Author.getAuthorsFromSearch(search, pageIndex, ITEMS_PER_PAGE));
        }
    }
}