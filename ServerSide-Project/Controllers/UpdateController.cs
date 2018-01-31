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

        [HttpPost]
        public ActionResult UpdateBook(Book book)
        {

            if (ModelState.IsValid) //validate the data
            {
                //int a = 5;
            }
            //THIS NEED TO BE CHANGED IF YOU EVER WANNT TO CHANGE ISBN NEED TO STORE ISBN id and send it also (if changed)
            
            //may intreduce bugs with null   Works for now
            var repo = Session["repo"] as Repository;
            repo.BookList.Where(d => d.ISBN == book.ISBN).First().SetBook(book);


            //redirect to list view when done
            return RedirectToAction("ListBooks", "List");
        }

        [HttpGet]
        public ActionResult GetAdminView()
        {
            Repository repo = (Repository)Session["repo"];
            return View("GetAdminView", repo.AdminList);
        }
    }
}