using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListBooks()
        {
            Repository repo = (Repository)Session["repo"];
            return View(repo.BookList);
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            Repository repo = (Repository)Session["repo"];
            return View(repo.BookList.FirstOrDefault(x => x.ISBN == id));
        }

        [HttpGet]
        public ActionResult ListAuthors()
        {
            Repository repo = (Repository)Session["repo"];
            return View("ListAuthors",repo.AuthorList);
        }

        [HttpGet]
        public ActionResult ListAuthorDetails(string id)
        {
            Repository repo = (Repository)Session["repo"];
            return View("ListAuthorDetails", repo.AuthorList.FirstOrDefault(x => x.ID == id));
        }

    }
}