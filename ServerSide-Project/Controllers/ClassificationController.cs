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

        [HttpGet]
        public ActionResult CreateClassification()
        {
            return View("CreateClassification");
        }

        [HttpPost]
        [ActionName("CreateClassification")]
        public ActionResult CreateClassificationPost(Classification classification)
        {
            if (Classification.createClassification(classification))
                return RedirectToAction("BrowseAllBooks", "Books", null);
            else
                return RedirectToAction("BrowseAllBooks", "Books", null);
        }

        [HttpPost]
        public ActionResult deleteClassification(int id)
        {
            int a = 0;
            if (Classification.deleteClassification(Classification.getClassificationFromID(id)))
                return RedirectToAction("BrowseAllBooks", "Books", null);
            else
                return RedirectToAction("BrowseAllBooks", "Books", null);
        }

        public ActionResult getClassification()
        {
            return PartialView("Classification", Classification.getAllClassifications());
        }
    }
}