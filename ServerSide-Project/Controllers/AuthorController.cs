using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using Service.Models;
using Service.Managers;
using ServerSide_Project.Tools;

namespace ServerSide_Project.Controllers
{
    public class AuthorController : ControllerExtension
    {
        public const int ITEMS_PER_PAGE = 15;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAuthor()
        {
            ValidateAndRedirect(Rank.Admin);
            return View("CreateAuthor");
        }

        [HttpPost]
        public ActionResult CreateAuthor(Author author)
        {
            ValidateAndRedirect(Rank.Admin);
            if (AuthorManager.CreateAuthor(author) != 0)
                return RedirectToAction("ListAuthorDetails", "Author", new { id = AuthorManager.CreateAuthor(author)});
            else
                return RedirectToAction("BrowseAllAuthors", "Author", null);
        }

        [HttpGet]
        public ActionResult EditAuthor(int id)
        {
            ValidateAndRedirect(Rank.Admin);
            return View("EditAuthor", AuthorManager.GetAuthorFromID(id));
        }

        [HttpPost]
        public ActionResult EditAuthor(Author author)
        {
            ValidateAndRedirect(Rank.Admin);
            return RedirectToAction("ListAuthorDetails", "Author", new { id = AuthorManager.EditAuthor(author).Aid });
        }
        
        [HttpPost]
        public ActionResult DeleteAuthor(int id)
        {
            ValidateAndRedirect(Rank.Admin);
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