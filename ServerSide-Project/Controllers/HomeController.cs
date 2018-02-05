using ServerSide_Project.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            ServerSide_Project.Models.Repository repo = new ServerSide_Project.Models.Repository();
            Session["repo"] = repo;

            using (var db = new dbGrupp3())
            {
                //db.Database.Connection.Open();
                foreach (var book in db.BOOKs)
                {
                    Console.WriteLine(book.Title);
                }
            }

            return View("Home");

        }
    }
}