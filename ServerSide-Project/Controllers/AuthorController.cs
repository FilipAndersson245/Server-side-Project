using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}