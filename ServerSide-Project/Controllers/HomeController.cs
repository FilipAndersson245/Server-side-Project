﻿using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            Repository repo = new Repository();
            Session["repo"] = repo;
            return View("Home");
        }
    }
}