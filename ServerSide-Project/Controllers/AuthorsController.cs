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
            int aID = Author.CreateAuthor(author);
            if ( aID != 0)
                return RedirectToAction("ListAuthorDetails", "Authors", new { id = aID});
            else
                return RedirectToAction("BrowseAllAuthors", "Authors", null);
        }

        [HttpGet]
        public ActionResult updateAuthor(int id)
        {
            Author author = Author.getAuthorFromID(id);
            return View("updateAuthor", author);
        }

        [HttpPost]
        [ActionName("updateAuthor")]
        public ActionResult updateAuthorPost(Author author)
        {
            Author editedAuthor = Author.updateAuthor(author);
            return RedirectToAction("ListAuthorDetails", "Authors", new { id = editedAuthor.Aid });
        }
        
        [HttpPost]
        public ActionResult deleteAuthor(int id)
        {
            if (Author.deleteAuthor(Author.getAuthorFromID(id)))
                return RedirectToAction("BrowseAllAuthors", "Authors", null);
            else
                return RedirectToAction("BrowseAllAuthors", "Authors", null);
        }

        [HttpGet]
        public ActionResult BrowseAllAuthors(int? page)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var authorList = Author.getAllAuthors(pageIndex, ITEMS_PER_PAGE);
            return View("BrowseAllAuthors", authorList);
        }

        [HttpGet]
        public ActionResult ListAuthorDetails(int id, int? bookPage)
        {
            int bookPageIndex = bookPage.HasValue ? Convert.ToInt32(bookPage) : 1;
            Author author = Author.getAuthorDetails(id, bookPageIndex);
            return View("ListAuthorDetails", author);
        }

        [HttpGet]
        public ActionResult searchAuthors(int? page, string search)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            return View("BrowseSearchedAuthors", Author.getAuthorsFromSearch(search, pageIndex, ITEMS_PER_PAGE));
        }
    }
}