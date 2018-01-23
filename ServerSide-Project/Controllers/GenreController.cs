using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}