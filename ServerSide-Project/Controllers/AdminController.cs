using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Service.Managers;
using ServerSide_Project.Tools;

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
        public ActionResult CreateAdmin()
        {
            ValidateAndRedirect(Rank.SuperAdmin);
            return View("CreateAdmin");
        }

        [HttpPost]
        public ActionResult CreateAdmin(Admin admin)
        {
            ValidateAndRedirect(Rank.SuperAdmin);
            if (ModelState.IsValid)
            {
                //todo check if already exist
                using (var deriveBytes = new Rfc2898DeriveBytes(admin.Password, 20))
                {
                    Admin newAdmin = new Admin
                    {
                        Salt = Convert.ToBase64String(deriveBytes.Salt),
                        PasswordHash = Convert.ToBase64String(deriveBytes.GetBytes(20)),
                        Username = admin.Username,
                        PermissionLevel = admin.PermissionLevel
                    };
                    AdminManager adminManager = new AdminManager();
                    adminManager.CreateAdmin(newAdmin);
                }
            }
            else
            {
                throw new Exception("ModelstateNotValid");
            }
            return RedirectToAction("AdminPanel", "Admin", null);
        }

        [HttpPost]
        public ActionResult DeleteAdmin(string id)
        {
            ValidateAndRedirect(Rank.SuperAdmin);
            return RedirectToAction("AdminPanel", "Admin", null);
        }

        [HttpGet]
        public ActionResult Login(string returnBackTo = null)
        {
            ViewBag.returnBackTo = returnBackTo;
            return View("login");
        }

        [HttpPost]
        public ActionResult Login(Admin admin, string returnBackTo = null)
        {
            AdminManager adminManager = new AdminManager();
            var serverAdmin = adminManager.GetAdmin(admin.Username);
            byte[] salt = Convert.FromBase64String(serverAdmin.Salt);
            byte[] key = Convert.FromBase64String(serverAdmin.PasswordHash);
            //load salt and key from database
            using (var deriveBytes = new Rfc2898DeriveBytes(admin.Password, salt))
            {
                byte[] newKey = deriveBytes.GetBytes(20);  // derive a 20-byte key
                if (newKey.SequenceEqual(key))
                {
                    Session["authentication"] = admin.Username;
                    Session["level"] = admin.PermissionLevel;
                    if (returnBackTo.Equals(""))
                        return RedirectToAction("index", "Home");
                    return Redirect(returnBackTo);
                }
                return RedirectToAction("Login","Admin",new { returnBackTo });
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