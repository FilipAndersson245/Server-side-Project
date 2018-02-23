﻿using ServerSide_Project.Models;
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
                    newAdmin.Salt = Convert.ToBase64String(deriveBytes.Salt);
                    newAdmin.PasswordHash = Convert.ToBase64String(deriveBytes.GetBytes(20));
                    newAdmin.Username = admin.Username;
                    newAdmin.PermissionLevel = admin.PermissionLevel;
                    Admin.createAdmin(newAdmin);
                }
            }
            else
            {
                throw new NotFiniteNumberException("ModelstateNotValid");
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
                }
            }
            

            return RedirectToAction("Index", "Home"); //change maybe
        }

        [HttpGet]
        public ActionResult AdminPanel()
        {
            if(Session["authentication"] == null)
            {
                // return RedirectToAction("login", new { redirectBackToAction = this.ControllerContext.RouteData.Values["controller"].ToString(), RedirectToController = this.ControllerContext.RouteData.Values["controller"]});
                return RedirectToAction("login");
            }
            else
            {
                return View("AdminPanel", Admin.getAllAdmins());
            }
            
        }



    }
}