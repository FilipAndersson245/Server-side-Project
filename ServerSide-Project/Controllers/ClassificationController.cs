using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using Service.Managers;

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
            return View("ListBooks", ClassificationManager.GetBooksByClassification(id));
        }

        [HttpGet]
        public ActionResult EditClassification(int id)
        {
            return View("EditClassification", ClassificationManager.GetClassificationFromID(id));
        }

        [HttpPost]
        [ActionName("editClassification")]
        public ActionResult editClassificationPost(Classification classification)
        {
            if(ClassificationManager.EditClassification(classification))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
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
            if (ClassificationManager.CreateClassification(classification))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpPost]
        public ActionResult DeleteClassification(int id)
        {
            if (ClassificationManager.DeleteClassification(ClassificationManager.GetClassificationFromID(id)))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult GetClassification()
        {
            return PartialView("Classification", ClassificationManager.GetAllClassifications());
        }
    }
}