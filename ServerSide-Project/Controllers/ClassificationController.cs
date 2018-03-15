using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class ClassificationController : ControllerExtension
    {
        [HttpGet]
        public ActionResult ListClassifications(int id)
        {
            ClassificationManager classificationManager = new ClassificationManager();
            return View("ListBooks", classificationManager.GetBooksByClassification(id));
        }

        [HttpGet]
        public ActionResult EditClassification(int id)
        {
            AuthorizeAndRedirect();
            ClassificationManager classificationManager = new ClassificationManager();
            return View("EditClassification", classificationManager.GetClassificationFromID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClassification(Classification classification)
        {
            AuthorizeAndRedirect();
            ClassificationManager classificationManager = new ClassificationManager();
            var validation = classificationManager.EditClassification(classification);
            if (validation.IsValid)
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
            {
                ValidationMessages.ConvertCodeToMsg(ModelState, validation.ErrorDict);
                return RedirectToAction("BrowseAllBooks", "Book", null);
            }
        }

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult CreateClassification()
        {
            AuthorizeAndRedirect();
            return View("CreateClassification");
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClassification(Classification classification)
        {
            AuthorizeAndRedirect();
            ClassificationManager classificationManager = new ClassificationManager();
            var validation = classificationManager.CreateClassification(classification);
            if (validation.IsValid)
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                ValidationMessages.ConvertCodeToMsg(ModelState, validation.ErrorDict);
            return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteClassificationPost(int id)
        {
            AuthorizeAndRedirect();
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