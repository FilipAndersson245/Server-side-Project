using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Models
{
    public class Book : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
    }
}