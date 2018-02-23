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
    public class AuthorController : Controller
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
            if (AuthorManager.CreateAuthor(author) != 0)
                return RedirectToAction("ListAuthorDetails", "Author", new { id = AuthorManager.CreateAuthor(author)});
            else
                return RedirectToAction("BrowseAllAuthors", "Author", null);
        }

        [HttpGet]
        public ActionResult EditAuthor(int id)
        {
            return View("EditAuthor", AuthorManager.GetAuthorFromID(id));
        }

        [HttpPost]
        [ActionName("EditAuthor")]
        public ActionResult EditAuthorPost(Author author)
        {
            return RedirectToAction("ListAuthorDetails", "Author", new { id = AuthorManager.EditAuthor(author).Aid });
        }
        
        [HttpPost]
        public ActionResult DeleteAuthor(int id)
        {
            if (AuthorManager.DeleteAuthor(AuthorManager.GetAuthorFromID(id)))
                return RedirectToAction("BrowseAllAuthors", "Author", null);
            else
                return RedirectToAction("BrowseAllAuthors", "Author", null);
        }

        [HttpGet]
        public ActionResult BrowseAllAuthors(int page = 1)
        {
            return View("BrowseAllAuthors", AuthorManager.GetAllAuthors(page, ITEMS_PER_PAGE));
        }

        [HttpGet]
        public ActionResult ListAuthorDetails(int id, int bookPage = 1)
        {
            return View("ListAuthorDetails", AuthorManager.GetAuthorDetails(id, bookPage));
        }

        [HttpGet]
        public ActionResult SearchAuthors(string search, int page = 1)
        {
            return View("BrowseSearchedAuthors", AuthorManager.GetAuthorsFromSearch(search, page, ITEMS_PER_PAGE));
        }
    }
}