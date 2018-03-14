using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System;
using System.Linq;
using System.Web.Mvc;

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
            //ValidateAndRedirect(Rank.SuperAdmin);
            return View("CreateAdmin");
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(Admin admin)
        {
            //ValidateAndRedirect(Rank.SuperAdmin);
            AdminManager manager = new AdminManager();
            var valid = manager.SignUp(admin);
            if (valid.IsValid)
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            ValidationMessages.ConvertCodeToMsg(ModelState, valid.ErrorDict);
            return RedirectToAction("CreateAdmin", "Admin", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAdmin(string id)
        {
            ValidateAndRedirect(Rank.SuperAdmin);
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
            if (validation.IsValid)
            {

                Session["authentication"] = admin.Username;
                Session["level"] = manager.getPermissionLevel(admin.Username);
                if (String.IsNullOrEmpty(returnBackTo))
                    return RedirectToAction("index", "Home");
                return Redirect(returnBackTo);
            }
            else
            {
                ValidationMessages.ConvertCodeToMsg(ModelState, validation.ErrorDict);
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
            ValidateAndRedirect();
            AdminManager adminManager = new AdminManager();
            return View("AdminPanel", adminManager.GetAllAdmins());
        }
    }
}