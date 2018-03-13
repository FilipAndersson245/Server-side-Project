using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System;
using System.Web.Mvc;

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
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthor(Author author)
        {
            ValidateAndRedirect();
            AuthorManager authorManager = new AuthorManager();
            var authorTuple = authorManager.CreateAuthor(author);
            if (authorTuple.Item1 != null)
                return RedirectToAction("ListAuthorDetails", "Author", new { authorTuple.Item1 });
            else
            {
                ValidationMessages.ConvertCodeToMsg(ModelState, authorTuple.Item2.ErrorDict);
                return RedirectToAction("CreateAuthor", "Author");
            }
        }

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult EditAuthor(int id)
        {
            ValidateAndRedirect();
            AuthorManager authorManager = new AuthorManager();
            return View("EditAuthor", authorManager.GetAuthorFromID(id));
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuthor(Author author)
        {
            ValidateAndRedirect();
            AuthorManager manager = new AuthorManager();
            var authorTuple = manager.EditAuthor(author);
            if (authorTuple.Item1 != null)
            {
                return RedirectToAction("ListAuthorDetails", "Author", new { id = Convert.ToInt32(authorTuple.Item1.Aid) });
            }
            ValidationMessages.ConvertCodeToMsg(ModelState, authorTuple.Item2.ErrorDict);
            return RedirectToAction("BrowseAllAuthors", "Author");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAuthorPost(int id)
        {
            ValidateAndRedirect();
            AuthorManager authorManager = new AuthorManager();
            if (authorManager.DeleteAuthor(authorManager.GetAuthorFromID(id)))
                return RedirectToAction("BrowseAllAuthors", "Author", null);
            else
                return RedirectToAction("BrowseAllAuthors", "Author", null);
        }

        [HttpGet]
        public ActionResult DeleteAuthor(string id)
        {
            AuthorManager manager = new AuthorManager();
            return View("DeleteAuthor", manager.GetAuthorFromID(Convert.ToInt32(id)));
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
        public ActionResult SearchAuthors(Search search, int page = 1)
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