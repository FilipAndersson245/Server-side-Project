using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Service.Tools;

namespace ServerSide_Project.Controllers
{
    public class AdminController : ControllerExtension
    {
        // GET: Admins
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult CreateAdmin()
        {
            AuthorizeAndRedirect(Rank.SuperAdmin);
            return View("CreateAdmin");
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(Admin admin)
        {
            AuthorizeAndRedirect(Rank.SuperAdmin);
            AdminManager manager = new AdminManager();
            var valid = manager.SignUp(admin);
            if (valid.IsValid)
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            ValidationMessages.ConvertCodeToMsg(ModelState, valid.ErrorDict);
            return RedirectToAction("CreateAdmin", "Admin", null);
        }

        [HttpGet]
        public ActionResult EditAdmin(string id)
        {
            AuthorizeAndRedirect(Rank.Admin);
            AdminManager manager = new AdminManager();
            return View("Editadmin", manager.GetAdmin(id));
        }

        [HttpPost]
        public ActionResult EditAdminPost(Admin admin)
        {
            AuthorizeAndRedirect(Rank.Admin);
            AdminManager manager = new AdminManager();
            Admin oldAdmin = manager.GetAdmin(admin.Username);
            admin.Username = oldAdmin.Username;
            if ((Rank)Session["Level"] < Rank.SuperAdmin) //Don't allow changing of admin level if admin who edited is not superadmin
                admin.PermissionLevel = oldAdmin.PermissionLevel;
            if (admin.Password == null)
            {
                admin.PasswordHash = oldAdmin.PasswordHash;
                admin.Salt = oldAdmin.Salt;
                admin.Password = "Tjollahopp1";
            }
            else
            {
                Hashing hashing = new Hashing(admin.Password);
                admin.PasswordHash = hashing.Hash;
                admin.Salt = hashing.Salt;
            }
            manager.EditAdmin(admin);
            return RedirectToAction("AdminPanel", "Admin", null);
        }


        [HttpGet]
        public ActionResult DeleteAdmin(string id)
        {
            AuthorizeAndRedirect(Rank.SuperAdmin);
            AdminManager manager = new AdminManager();
            Admin admin = manager.GetAdmin(id);
            return View("DeleteAdmin", admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAdminPost(string id)
        {
            AuthorizeAndRedirect(Rank.SuperAdmin);
            AdminManager manager = new AdminManager();
            if (manager.DeleteAdmin(id))
                return RedirectToAction("AdminPanel", "Admin", null);
            else
                return RedirectToAction("AdminPanel", "Admin", null);
        }

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult Login(string returnBackTo = null)
        {
            ViewBag.returnBackTo = returnBackTo;
            return View("Login");
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin, string returnBackTo = null)
        {
            var modelList = ModelState.ToList();
            var manager = new AdminManager();
            var validation = manager.Login(admin);
            if (validation.Item2.IsValid)
            {
                Session["authentication"] = admin.Username;
                Session["level"] = manager.getPermissionLevel(admin.Username);
                Session["classificationEditor"] = validation.Item1.CanEditClassifications;
                if (String.IsNullOrEmpty(returnBackTo))
                    return RedirectToAction("index", "Home");
                return Redirect(returnBackTo);
            }
            else
            {
                ValidationMessages.ConvertCodeToMsg(ModelState, validation.Item2.ErrorDict);
                return RedirectToAction("Login", new { returnBackTo });
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AdminPanel()
        {
            AuthorizeAndRedirect();
            AdminManager adminManager = new AdminManager();
            return View("AdminPanel", adminManager.GetAllAdmins());
        }
    }
}