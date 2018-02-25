﻿using System;
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
            return View("ListBooks", ClassificationManager.GetBooksByClassification(id));
        }

        [HttpGet]
        public ActionResult EditClassification(int id)
        {
            ValidateAndRedirect();
            return View("EditClassification", ClassificationManager.GetClassificationFromID(id));
        }

        [HttpPost]
        public ActionResult editClassification(Classification classification)
        {
            ValidateAndRedirect();
            if (ClassificationManager.EditClassification(classification))
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
            if (ClassificationManager.CreateClassification(classification))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpPost]
        public ActionResult DeleteClassification(int id)
        {
            ValidateAndRedirect();
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