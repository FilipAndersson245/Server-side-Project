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
            ValidateAndRedirect(Rank.SuperAdmin);
            return View("CreateAdmin");
        }

        [HttpPost]
        [SetTempDataModelState]
        public ActionResult CreateAdmin(Admin admin)
        {
            ValidateAndRedirect(Rank.SuperAdmin);
            AdminManager manager = new AdminManager();
            if (manager.SignUp(ModelState,admin))
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            return RedirectToAction("CreateAdmin", "Admin", null);
        }

        [HttpPost]
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
        public ActionResult Login(Admin admin, string returnBackTo = null)
        {
            var modelList = ModelState.ToList();
            if (new AdminManager().Login(modelList, admin.Username, admin.Password))
            {
                Session["authentication"] = admin.Username;
                Session["level"] = admin.PermissionLevel;
                if (String.IsNullOrEmpty(returnBackTo))
                    return RedirectToAction("index", "Home");
                return Redirect(returnBackTo);
            }
            else
            {
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