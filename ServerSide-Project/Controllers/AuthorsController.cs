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
            Repository repo = (Repository)Session["repo"];
            repo.AuthorList.Add(author);
            return RedirectToAction("ListAuthors", "Authors", null);
        }

        
        public ActionResult DeleteAuthor(string id)
        {
            Repository repo = (Repository)Session["repo"];
            repo.AuthorList.RemoveAll(x => x.ID == id);
            return RedirectToAction("ListAuthors", "Authors", null);
        }

        [HttpGet]
        public ActionResult ListAuthors()
        {
            Repository repo = (Repository)Session["repo"];
            return View("ListAuthors", repo.AuthorList);
        }

        [HttpGet]
        public ActionResult ListAuthorDetails(string id)
        {
            Repository repo = (Repository)Session["repo"];
            return View("ListAuthorDetails", repo.AuthorList.FirstOrDefault(x => x.ID == id));
        }
    }
}