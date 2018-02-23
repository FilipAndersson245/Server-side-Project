using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using Service.Models;
using Service.Managers;

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
            int aID = AuthorManager.CreateAuthor(author);
            if ( aID != 0)
                return RedirectToAction("ListAuthorDetails", "Authors", new { id = aID});
            else
                return RedirectToAction("BrowseAllAuthors", "Authors", null);
        }

        [HttpGet]
        public ActionResult updateAuthor(int id)
        {
            return View("updateAuthor", AuthorManager.getAuthorFromID(id));
        }

        [HttpPost]
        [ActionName("updateAuthor")]
        public ActionResult updateAuthorPost(Author author)
        {
            Author editedAuthor = AuthorManager.updateAuthor(author);
            return RedirectToAction("ListAuthorDetails", "Authors", new { id = editedAuthor.Aid });
        }
        
        [HttpPost]
        public ActionResult deleteAuthor(int id)
        {
            if (AuthorManager.deleteAuthor(AuthorManager.getAuthorFromID(id)))
                return RedirectToAction("BrowseAllAuthors", "Authors", null);
            else
                return RedirectToAction("BrowseAllAuthors", "Authors", null);
        }

        [HttpGet]
        public ActionResult BrowseAllAuthors(int? page)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var authorList = AuthorManager.getAllAuthors(pageIndex, ITEMS_PER_PAGE);
            return View("BrowseAllAuthors", authorList);
        }

        [HttpGet]
        public ActionResult ListAuthorDetails(int id, int? bookPage)
        {
            int bookPageIndex = bookPage.HasValue ? Convert.ToInt32(bookPage) : 1;
            Author author = AuthorManager.getAuthorDetails(id, bookPageIndex);
            return View("ListAuthorDetails", author);
        }

        [HttpGet]
        public ActionResult searchAuthors(int? page, string search)
        {
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            return View("BrowseSearchedAuthors", AuthorManager.getAuthorsFromSearch(search, pageIndex, ITEMS_PER_PAGE));
        }
    }
}