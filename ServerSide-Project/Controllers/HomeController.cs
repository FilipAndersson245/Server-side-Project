using ServerSide_Project.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

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

            return View("Home");

        }
    }
}