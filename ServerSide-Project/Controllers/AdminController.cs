﻿using Service.Models;
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
            ValidateAndRedirect(Rank.SuperAdmin);
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
                if (newKey.SequenceEqual(key))
                {
                    Session["authentication"] = admin.Username;
                    Session["level"] = admin.PermissionLevel;
                    return RedirectToAction("Index", "Home");
                }
                return View("Login");
            }


            
        }

        [HttpGet]
        public ActionResult AdminPanel()
        {
            ValidateAndRedirect();
            // return RedirectToAction("login", new { redirectBackToAction = this.ControllerContext.RouteData.Values["controller"].ToString(), RedirectToController = this.ControllerContext.RouteData.Values["controller"]});
            return View("AdminPanel", AdminManager.GetAllAdmins());

        }


    }

    //move to a different folder not in here
    


}