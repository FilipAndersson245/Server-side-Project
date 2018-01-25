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


    }
}