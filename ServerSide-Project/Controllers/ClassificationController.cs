using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System.Linq;
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
            ClassificationManager manager = new ClassificationManager();
            var validation = manager.DeleteClassification(manager.GetClassificationFromID(id));
            if (validation.IsValid)
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
            {
                ValidationMessages.ConvertCodeToMsg(ModelState, validation.ErrorDict);
                return View("DeleteClassification", manager.GetClassificationFromID(id));
            } 
        }

        [HttpGet]
        public ActionResult DeleteClassification(int id)
        {
            ClassificationManager manager = new ClassificationManager();
            return View("DeleteClassification", manager.GetClassificationFromID(id));
        }

        [HttpGet]
        public ActionResult GetClassifications(int[] classifications = null)
        {
            ClassificationManager classificationManager = new ClassificationManager();
            var a = new ListClassification() { Classifications = classificationManager.GetAllClassifications(), SelectedClassification = classifications?.ToList() };
            return PartialView("ListClassifications",a );
        }

        [HttpGet]
        public ActionResult GetClassificationDropdown()
        {
            ClassificationManager classificationManager = new ClassificationManager();
            return PartialView("ClassificationDropdown", classificationManager.GetAllClassifications());
        }
    }
}