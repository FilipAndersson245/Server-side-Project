﻿using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using Service.Validations;
using System;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class AuthorController : ControllerExtension
    {
        private const int ITEMS_PER_PAGE = 15;
        private AuthorManager Manager { get; } = new AuthorManager();

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult CreateAuthor()
        {
            AuthorizeAndRedirect();
            return View("CreateAuthor");
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthor(Author author)
        {
            AuthorizeAndRedirect();
            Tuple<Author, AuthorValidation> authorTuple = Manager.CreateAuthor(author);
            
            if (authorTuple.Item1 != null)
            {
                return RedirectToAction("ListAuthorDetails", "Author", new { id = Convert.ToInt32(authorTuple.Item1.Aid) });
            }
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
            AuthorizeAndRedirect();
            return View("EditAuthor", Manager.GetAuthorFromID(id));
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuthor(Author author)
        {
            AuthorizeAndRedirect();
            Tuple<Author, AuthorValidation> authorTuple = Manager.EditAuthor(author);
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
            AuthorizeAndRedirect();
            if (Manager.DeleteAuthor(Manager.GetAuthorFromID(id)))
                return RedirectToAction("BrowseAllAuthors", "Author", null);
            else
                return RedirectToAction("BrowseAllAuthors", "Author", null);
        }

        [HttpGet]
        public ActionResult DeleteAuthor(string id)
        {
            return View("DeleteAuthor", Manager.GetAuthorFromID(Convert.ToInt32(id)));
        }

        [HttpGet]
        public ActionResult BrowseAllAuthors(int page = 1)
        {
            return View("BrowseAllAuthors", Manager.GetAllAuthors(page, ITEMS_PER_PAGE));
        }

        [HttpGet]
        public ActionResult ListAuthorDetails(int id, int bookPage = 1)
        {
            return View("ListAuthorDetails", Manager.GetAuthorDetails(id, bookPage));
        }

        [HttpGet]
        public ActionResult SearchAuthors(string search, int page = 1)
        {
            return View("BrowseSearchedAuthors", Manager.GetAuthorsFromSearch(search, page, ITEMS_PER_PAGE));
        }
    }
}