using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class DeleteController : Controller
    {
        // GET: Delete
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteBook(string id)
        {
            Repository repo = (Repository)Session["repo"];
            repo.BookList.RemoveAll(x => x.ISBN == id);
            return RedirectToAction("ListBooks", "List", null);
        }

        public ActionResult DeleteAuthor(string id)
        {
            Repository repo = (Repository)Session["repo"];
            repo.AuthorList.RemoveAll(x => x.ID == id);
            return RedirectToAction("ListAuthors", "List", null);
        }

        public ActionResult DeleteAdmin(string id)
        {
            Repository repo = (Repository)Session["repo"];
            repo.AdminList.RemoveAll(x => x.Username == id);
            return RedirectToAction("ListAdmins", "List", null);
        }
    }
}