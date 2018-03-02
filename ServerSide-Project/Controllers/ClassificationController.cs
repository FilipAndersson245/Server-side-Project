using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using Service.Managers;
using ServerSide_Project.Tools;

namespace ServerSide_Project.Controllers
{
    public class ClassificationController : ControllerExtension
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListClassifications(int id)
        {
            ClassificationManager classificationManager = new ClassificationManager();
            return View("ListBooks", classificationManager.GetBooksByClassification(id));
        }

        [HttpGet]
        public ActionResult EditClassification(int id)
        {
            ValidateAndRedirect();
            ClassificationManager classificationManager = new ClassificationManager();
            return View("EditClassification", classificationManager.GetClassificationFromID(id));
        }

        [HttpPost]
        public ActionResult EditClassification(Classification classification)
        {
            ValidateAndRedirect();
            ClassificationManager classificationManager = new ClassificationManager();
            if (classificationManager.EditClassification(classification))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult CreateClassification()
        {
            ValidateAndRedirect();
            return View("CreateClassification");
        }

        [HttpPost]
        public ActionResult CreateClassification(Classification classification)
        {
            ValidateAndRedirect();
            ClassificationManager classificationManager = new ClassificationManager();
            if (classificationManager.CreateClassification(classification))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpPost]
        public ActionResult DeleteClassificationPost(int id)
        {
            ValidateAndRedirect();
            ClassificationManager classificationManager = new ClassificationManager();
            if (classificationManager.DeleteClassification(classificationManager.GetClassificationFromID(id)))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult DeleteClassification(int id)
        {
            ClassificationManager manager = new ClassificationManager();
            return View("DeleteClassification", manager.GetClassificationFromID(id));
        }

        [HttpGet]
        public ActionResult GetClassifications()
        {
            ClassificationManager classificationManager = new ClassificationManager();
            return PartialView("ListClassifications", classificationManager.GetAllClassifications());
        }

        [HttpGet]
        public ActionResult GetClassificationDropdown()
        {
            ClassificationManager classificationManager = new ClassificationManager();
            return PartialView("ClassificationDropdown", classificationManager.GetAllClassifications());
        }
    }
}