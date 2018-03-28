using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using Service.Validations;
using System.Linq;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class ClassificationController : ControllerExtension
    {
        private ClassificationManager _Manager { get; } = new ClassificationManager();

        [HttpGet]
        public ActionResult ListClassifications(int id)
        {
            return View("ListBooks", _Manager.GetBooksByClassification(id));
        }

        [HttpGet]
        public ActionResult EditClassification(int id)
        {
            AuthorizeAndRedirect();
            return View("EditClassification", _Manager.GetClassificationFromID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClassification(Classification classification)
        {
            AuthorizeAndRedirect();
            ClassificationValidation validation = _Manager.EditClassification(classification);
            if (!validation.IsValid)
                ValidationMessages.ConvertCodeToMsg(ModelState, validation.ErrorDict);
            else
                ModelState.Clear();
            return RedirectToAction("BrowseAllBooks", "Book", null);
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
            ClassificationValidation validation = _Manager.CreateClassification(classification);
            if (!validation.IsValid)
            {
                ModelState.Clear();
                ValidationMessages.ConvertCodeToMsg(ModelState, validation.ErrorDict);
            }
            return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteClassificationPost(int id)
        {
            AuthorizeAndRedirect();
            ClassificationValidation validation = _Manager.DeleteClassification(_Manager.GetClassificationFromID(id));
            if (validation.IsValid)
                return RedirectToAction("BrowseAllBooks", "Book", null);
            ValidationMessages.ConvertCodeToMsg(ModelState, validation.ErrorDict);
            return View("DeleteClassification", _Manager.GetClassificationFromID(id));
        }

        [HttpGet]
        public ActionResult DeleteClassification(int id)
        {
            AuthorizeAndRedirect();
            return View("DeleteClassification", _Manager.GetClassificationFromID(id));
        }

        [HttpGet]
        public ActionResult GetClassifications(int[] classifications = null)
        {
            return PartialView("ListClassifications", new ListClassification()
            {
                Classifications = _Manager.GetAllClassifications(),
                SelectedClassification = classifications?.ToList()
            });
        }

        [HttpGet]
        public ActionResult GetClassificationDropdown()
        {
            return PartialView("ClassificationDropdown", _Manager.GetAllClassifications());
        }
    }
}