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
        [RestoreModelStateFromTempData]
        public ActionResult CreateAuthor()
        {
            ValidateAndRedirect();
            return View("CreateAuthor");
        }

        [HttpPost]
        [SetTempDataModelState]
        public ActionResult CreateAuthor(Author author)
        {
            ValidateAndRedirect();
            AuthorManager authorManager = new AuthorManager();
            var id = authorManager.CreateAuthor(ModelState, author);
            if (id != null)
                return RedirectToAction("ListAuthorDetails", "Author", new { id });
            else
                return RedirectToAction("CreateAuthor", "Author");
        }

        [HttpGet]
        public ActionResult EditAuthor(int id)
        {
            ValidateAndRedirect();
            AuthorManager authorManager = new AuthorManager();
            return View("EditAuthor", authorManager.GetAuthorFromID(id));
        }

        [HttpPost]
        public ActionResult EditAuthor(Author author)
        {
            ValidateAndRedirect();
            AuthorManager manager = new AuthorManager();
            var dbAuthor = manager.EditAuthor(ModelState, author);
            if (dbAuthor != null)
            {
                return RedirectToAction("ListAuthorDetails", "Author", new { dbAuthor.Aid });
            }
            //may not work
            return RedirectToAction("BrowseAllAuthors","Admin");
        }
        
        [HttpPost]
        public ActionResult DeleteAuthor(int id)
        {
            ValidateAndRedirect();
            AuthorManager authorManager = new AuthorManager();
            if (authorManager.DeleteAuthor(authorManager.GetAuthorFromID(id)))
                return RedirectToAction("BrowseAllAuthors", "Author", null);
            else
                return RedirectToAction("BrowseAllAuthors", "Author", null);
        }

        [HttpGet]
        public ActionResult BrowseAllAuthors(int page = 1)
        {
            AuthorManager authorManager = new AuthorManager();
            return View("BrowseAllAuthors", authorManager.GetAllAuthors(page, ITEMS_PER_PAGE));
        }

        [HttpGet]
        public ActionResult ListAuthorDetails(int id, int bookPage = 1)
        {
            AuthorManager authorManager = new AuthorManager();
            return View("ListAuthorDetails", authorManager.GetAuthorDetails(id, bookPage));
        }

        [HttpGet]
        public ActionResult SearchAuthors(string search, int page = 1)
        {
            AuthorManager authorManager = new AuthorManager();
            return View("BrowseSearchedAuthors", authorManager.GetAuthorsFromSearch(search, page, ITEMS_PER_PAGE));
        }

        [HttpGet]
        public ActionResult GetAllAuthorsDropdown()
        {
            AuthorManager authorManager = new AuthorManager();
            return PartialView("AuthorDropdown", authorManager.GetAllAuthorsToList());
        }
    }
}