using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class AuthorsController : Controller
    {
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
        public ActionResult ListAuthors()
        {
            var authorList = new List<Author>();
            authorList = Author.getAllAuthors();
            return View("ListAuthors", authorList);
        }

        [HttpGet]
        public ActionResult ListAuthorDetails(string id)
        {
            Author author = Author.getAuthorDetails(id);
            return View("ListAuthorDetails", author);
        }
    }
}