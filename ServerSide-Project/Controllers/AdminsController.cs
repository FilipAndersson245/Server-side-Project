using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class AdminsController : Controller
    {
        // GET: Admins
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View("CreateAdmin");
        }

        [HttpPost]
        public ActionResult CreateAdmin(Admin admin)
        {
            ServerSide_Project.Models.Repository repo = (ServerSide_Project.Models.Repository)Session["repo"];
            repo.AdminList.Add(admin);
            return RedirectToAction("AdminPanel", "Admins", null);
        }


        public ActionResult DeleteAdmin(string id)
        {
            ServerSide_Project.Models.Repository repo = (ServerSide_Project.Models.Repository)Session["repo"];
            repo.AdminList.RemoveAll(x => x.Username == id);
            return RedirectToAction("AdminPanel", "Admins", null);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("login");
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            //Roles.AddUserToRole(admin.Username, "admin");
            //if(Admin.IsInRole("admin"))
            //{
            //    var x = 5;
            //}

            return RedirectToAction("Index", "Home"); //change maybe
        }

        [HttpGet]
        public ActionResult AdminPanel()
        {
            ServerSide_Project.Models.Repository repo = (ServerSide_Project.Models.Repository)Session["repo"];
            return View("AdminPanel", repo.AdminList);
        }


    }
}