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
            var classificationList = new List<Book>();
            classificationList = Classification.GetBooksByClassification(id);
            return View("ListBooks", classificationList);
        }

        [HttpGet]
        public ActionResult editClassification(int id)
        {
            return View("EditClassification", ClassificationManager.getClassificationFromID(id));
        }

        [HttpPost]
        [ActionName("editClassification")]
        public ActionResult editClassificationPost(Classification classification)
        {
            if(ClassificationManager.editClassification(classification))
                return RedirectToAction("BrowseAllBooks", "Books", null);
            else
                return RedirectToAction("BrowseAllBooks", "Books", null);
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
            if (ClassificationManager.createClassification(classification))
                return RedirectToAction("BrowseAllBooks", "Books", null);
            else
                return RedirectToAction("BrowseAllBooks", "Books", null);
        }

        [HttpPost]
        public ActionResult deleteClassification(int id)
        {
            int a = 0;
            if (ClassificationManager.deleteClassification(ClassificationManager.getClassificationFromID(id)))
                return RedirectToAction("BrowseAllBooks", "Books", null);
            else
                return RedirectToAction("BrowseAllBooks", "Books", null);
        }

        [HttpGet]
        public ActionResult GetClassification()
        {
            return PartialView("Classification", ClassificationManager.getAllClassifications());
        }
    }
}