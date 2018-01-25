using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class UpdateController : Controller
    {
        // GET: Update
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("login");
        }

        public ActionResult EditBook(string id)
        {
            var repo = Session["repo"] as Repository;
            return View("EditBook",repo.BookList.Find(x => x.ISBN == id)); // ret a book with ISBN equal to id
        }
    }
}