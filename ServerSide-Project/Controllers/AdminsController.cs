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
            //byte[] bytes = Encoding.Unicode.GetBytes(admin.Password);

            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //var rand = new Random();
            //var salt = new string(Enumerable.Repeat(chars, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            //byte[] src = Encoding.Unicode.GetBytes(salt);
            //byte[] dst = new byte[src.Length + bytes.Length];
            //System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            //System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            //HashAlgorithm algorithm = HashAlgorithm.Create("SHA512");
            //byte[] inArray = algorithm.ComputeHash(dst);
            ////return Convert.ToBase64String(inArray);    
            ////return EncodePasswordMd5(Convert.ToBase64String(inArray));

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
            //Roles.AddUserToRole(admin.Username, "admin");
            //if(Admin.IsInRole("admin"))
            //{
            //    var x = 5;
            //}

            return RedirectToAction("Index", "Home"); //change maybe
        }

        [HttpGet]
        public ActionResult AdminPanel()
        {
            return View("AdminPanel");
        }


    }
}