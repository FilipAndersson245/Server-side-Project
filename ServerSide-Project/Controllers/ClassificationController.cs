using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServerSide_Project.Models;

namespace ServerSide_Project.Controllers
{
    public class ClassificationController : Controller
    {
        // GET: Classification
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListClassifications(int id)
        {
            var classificationList = new List<Book>();
            classificationList = Classification.GetBooksByClassification(id);
            return View("ListBooks", classificationList);
        }


        public ActionResult getClassification()
        {
            return PartialView("Classification", Classification.getAllClassifications());
        }
    }
}