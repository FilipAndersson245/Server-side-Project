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
        private AdminManager Manager { get; } = new AdminManager();

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
            var valid = Manager.SignUp(admin);
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
            return View("Editadmin", Manager.GetAdmin(id));
        }

        [HttpPost]
        public ActionResult EditAdminPost(Admin admin)
        {
            AuthorizeAndRedirect(Rank.Admin);
            Admin oldAdmin = Manager.GetAdmin(admin.Username);
            admin.Username = oldAdmin.Username;
            if ((Rank)Session["Level"] < Rank.SuperAdmin) //Don't allow changing of admin level if admin who edited is not superadmin
                admin.PermissionLevel = oldAdmin.PermissionLevel;
            if (admin.Password == null)
            {
                admin.PasswordHash = oldAdmin.PasswordHash;
                admin.Salt = oldAdmin.Salt;
                admin.Password = "PlaceHolder123";
            }
            else
            {
                Hashing hashing = new Hashing(admin.Password);
                admin.PasswordHash = hashing.Hash;
                admin.Salt = hashing.Salt;
            }
            Manager.EditAdmin(admin);
            return RedirectToAction("AdminPanel", "Admin", null);
        }

        [HttpGet]
        public ActionResult DeleteAdmin(string id)
        {
            AuthorizeAndRedirect(Rank.SuperAdmin);
            Admin admin = Manager.GetAdmin(id);
            return View("DeleteAdmin", admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAdminPost(string id)
        {
            AuthorizeAndRedirect(Rank.SuperAdmin);
            if (Manager.DeleteAdmin(id))
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
            var validation = Manager.Login(admin);
            if (validation.Item2.IsValid)
            {
                Session["authentication"] = admin.Username;
                Session["level"] = Manager.getPermissionLevel(admin.Username);
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
            return View("AdminPanel", Manager.GetAllAdmins());
        }
    }
}