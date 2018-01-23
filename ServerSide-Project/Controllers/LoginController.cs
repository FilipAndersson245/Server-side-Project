using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            
            return View(new User());
        }

        // Post login
        [HttpPost]
        public void Login(User user)
        {

            //validate with database if login is OK

        }
        
        private bool ValidatePassword(string pwd)
        {

            throw new NotImplementedException();
        }



    }
}