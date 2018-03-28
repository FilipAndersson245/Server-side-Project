using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Service.Tools;
using Service.Validations;

namespace ServerSide_Project.Controllers
{
    public class AdminController : ControllerExtension
    {
        private AdminManager _Manager { get; } = new AdminManager();

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
            AdminValidation valid = _Manager.SignUp(admin);
            if (valid.IsValid)
            {
                ViewData.ModelState.Clear();
                return RedirectToAction("AdminPanel", "Admin");
            }
            ValidationMessages.ConvertCodeToMsg(ModelState, valid.ErrorDict);
            return RedirectToAction("CreateAdmin", "Admin", null);
        }

        [HttpGet]
        public ActionResult EditAdmin(string id)
        {
            if (!(id == Session["authentication"].ToString()))
            {
                AuthorizeAndRedirect(Rank.Admin);
            }
            return View("Editadmin", _Manager.GetAdmin(id));
        }

        [HttpPost]
        public ActionResult EditAdminPost(Admin admin)
        {
            if (!(admin.Username == Session["authentication"].ToString())) //Allow the user to change their own password even if not admin or higher
            {
                AuthorizeAndRedirect(Rank.Admin);
            }
            Admin oldAdmin = _Manager.GetAdmin(admin.Username);
            if ((Rank)Session["Level"] < Rank.SuperAdmin) //Don't allow changing of admin level or classification access if admin who edited is not superadmin
            {
                admin.PermissionLevel = oldAdmin.PermissionLevel;
                admin.CanEditClassifications = oldAdmin.CanEditClassifications;
            }
            if (admin.Password == null)
            {
                admin.PasswordHash = oldAdmin.PasswordHash;
                admin.Salt = oldAdmin.Salt;
                AdminValidation validation = _Manager.EditAdmin(admin, true);
                if (validation.IsValid)
                {
                    ViewData.ModelState.Clear();
                    return RedirectToAction("AdminPanel", "Admin", null);
                }
            }
            else
            {
                Hashing hashing = new Hashing(admin.Password);
                admin.PasswordHash = hashing.Hash;
                admin.Salt = hashing.Salt;
                AdminValidation validation = _Manager.EditAdmin(admin);
                if (validation.IsValid)
                {
                    ViewData.ModelState.Clear();
                    return RedirectToAction("AdminPanel", "Admin", null);
                }
            }
            return RedirectToAction("EditAdmin", new { id = admin.Username });
        }

        [HttpGet]
        public ActionResult DeleteAdmin(string id)
        {
            AuthorizeAndRedirect(Rank.SuperAdmin);
            return View("DeleteAdmin", _Manager.GetAdmin(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAdminPost(string id)
        {
            AuthorizeAndRedirect(Rank.SuperAdmin);
            _Manager.DeleteAdmin(id);
            return RedirectToAction("AdminPanel", "Admin", null);
        }

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult Login(string returnBackTo = null)
        {
            ViewBag.returnBackTo = returnBackTo;
            return View("Login");
        }

        /// <summary>
        /// Login and start session, then return to previous location
        /// </summary>
        /// <param name="returnBackTo">String that saves your location previous to trying to login.</param>
        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin, string returnBackTo = null)
        {
            Tuple<Admin, AdminValidation> validation = _Manager.Login(admin);
            if (validation.Item2.IsValid)
            {
                ViewData.ModelState.Clear();
                Session["authentication"] = admin.Username;
                Session["level"] = _Manager.getPermissionLevel(admin.Username);
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
            return View("AdminPanel", _Manager.GetAllAdmins());
        }
    }
}