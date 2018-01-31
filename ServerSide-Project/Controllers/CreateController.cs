using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class CreateController : Controller
    {
        // GET: Create
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateBook()
        {
            return View("CreateBook");
        }

        [HttpPost]
        [ActionName("CreateBook")]              // cannot have equal names so using this to emulate same same
        public ActionResult CreateBookPost(Book book)    //
        {
            if (ModelState.IsValid)
            {
                //valid book!
            }
            Repository repo = (Repository)Session["repo"];
            repo.BookList.Add(book);

            return RedirectToAction("ListBooks", "List", null); //maybe to the created book instead of list
        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View("CreateAdmin");
        }

        [HttpPost]
        public ActionResult CreateAdmin(Admin admin)
        {

            return RedirectToAction("index", "Home");
        }
    }
}