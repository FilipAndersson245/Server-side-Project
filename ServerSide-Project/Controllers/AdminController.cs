using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Service.Managers;

namespace ServerSide_Project.Controllers
{
    public class AdminController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {

            if (filterContext.Exception is ValidationException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction("Login", new { returnBackTo = Request.RawUrl });
            }
        }

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
                    newAdmin.Salt = Convert.ToBase64String(deriveBytes.Salt);
                    newAdmin.PasswordHash = Convert.ToBase64String(deriveBytes.GetBytes(20));
                    newAdmin.Username = admin.Username;
                    newAdmin.PermissionLevel = admin.PermissionLevel;
                    AdminManager.CreateAdmin(newAdmin);
                }
            }
            else
            {
                throw new NotFiniteNumberException("ModelstateNotValid");
            }

            return RedirectToAction("AdminPanel", "Admin", null);
        }


        public ActionResult DeleteAdmin(string id)
        {
            return RedirectToAction("AdminPanel", "Admin", null);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("login");
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var serverAdmin = AdminManager.GetAdmin(admin.Username);
            byte[] salt = Convert.FromBase64String(serverAdmin.Salt);
            byte[] key = Convert.FromBase64String(serverAdmin.PasswordHash);
            //load salt and key from database
            using (var deriveBytes = new Rfc2898DeriveBytes(admin.Password, salt))
            {
                byte[] newKey = deriveBytes.GetBytes(20);  // derive a 20-byte key
                if (!newKey.SequenceEqual(key))
                    throw new InvalidOperationException("Password is invalid!");
                else
                {
                    //System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                    Session["authentication"] = admin.Username;
                    Session["level"] = admin.PermissionLevel;
                }
            }


            return RedirectToAction("Index", "Home"); //change maybe
        }

        [HttpGet]
        public ActionResult AdminPanel()
        {
            ValidateAndRedirect();
            // return RedirectToAction("login", new { redirectBackToAction = this.ControllerContext.RouteData.Values["controller"].ToString(), RedirectToController = this.ControllerContext.RouteData.Values["controller"]});
            return View("AdminPanel", AdminManager.GetAllAdmins());

        }

        // place this in a extension of the Controller class so all controller can use it
        private bool ValidateAndRedirect(int level = 0)
        {
            if (Session["authentication"] == null || (int)Session["level"] < level) //change int to the level enum must include that inside wherever we place this function
            {
                //RedirectToAction("login", new { redirectTo = Request.RawUrl});
                throw new ValidationException();
            }
            return true;
        }


    }

    //move to a different folder not in here
    public class ValidationException : Exception { }


}