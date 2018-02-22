using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            if (ModelState.IsValid)
            {
                //todo check if already exist
                using (var deriveBytes = new Rfc2898DeriveBytes(admin.Password, 20))
                {
                    Admin newAdmin = new Admin();
                    newAdmin.Salt = Encoding.UTF8.GetString(deriveBytes.Salt);
                    newAdmin.PasswordHash = Encoding.UTF8.GetString(deriveBytes.GetBytes(20));
                    newAdmin.Username = admin.Username;
                    newAdmin.PermissionLevel = admin.PermissionLevel;
                    Admin.createAdmin(newAdmin);
                }
            }

            return RedirectToAction("AdminPanel", "Admins", null);
        }


        public ActionResult DeleteAdmin(string id)
        {
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
            var serverAdmin = Admin.getAdmin(admin.Username);
            byte[] salt = Encoding.UTF8.GetBytes(serverAdmin.Salt);
            byte[] key = Encoding.UTF8.GetBytes(serverAdmin.PasswordHash);
            //load salt and key from database

            using (var deriveBytes = new Rfc2898DeriveBytes(admin.Password, salt))
            {
                byte[] newKey = deriveBytes.GetBytes(20);  // derive a 20-byte key

                if (!newKey.SequenceEqual(key))
                    throw new InvalidOperationException("Password is invalid!");
            }

            return RedirectToAction("Index", "Home"); //change maybe
        }

        [HttpGet]
        public ActionResult AdminPanel()
        {
            return View("AdminPanel",Admin.getAllAdmins());
        }


    }
}