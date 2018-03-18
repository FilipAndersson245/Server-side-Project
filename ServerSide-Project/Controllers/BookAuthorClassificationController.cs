using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using Service.Validations;
using System;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class BookAuthorClassificationController : ControllerExtension
    {
        private BookManager _BookManager { get; } = new BookManager();
        private BookAuthorClassificationManager _BookAuthorClassificationManager { get; } = new BookAuthorClassificationManager();

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult CreateBook()
        {
            var bac = _BookAuthorClassificationManager.Setup();
            return View("CreateBook", bac);
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(BookAuthorClassification bac, string[] authorChecklist, int? classificationRadio) //Strukturera om till servicelagret och lös classificationRadio null
        {
            AuthorizeAndRedirect();
            var bookTuple = _BookManager.CreateBook(bac, authorChecklist, classificationRadio);
            if (bookTuple.Item2.IsValid)
                return RedirectToAction("ListBookDetails", "Book", new { id = bookTuple.Item1.ISBN });
            else
            {
                ValidationMessages.ConvertCodeToMsg(ModelState, bookTuple.Item2.ErrorDict);
                return RedirectToAction("CreateBook", "BookAuthorClassification");
            }
        }

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult EditBook(string id)
        {
            var bac = _BookAuthorClassificationManager.Setup();
            bac.Book = _BookManager.GetBookFromIsbn(id);
            return View("EditBook", bac);
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(BookAuthorClassification bac, string[] authorChecklist, int classificationRadio)
        {
            AuthorizeAndRedirect();
            Tuple<Book, BookValidation> bookTuple = _BookManager.EditBook(bac, authorChecklist, classificationRadio);
            if (bookTuple.Item2.IsValid)
                return RedirectToAction("ListBookDetails", "Book", new { id = bookTuple.Item1.ISBN });
            else
            {
                ValidationMessages.ConvertCodeToMsg(ModelState, bookTuple.Item2.ErrorDict);
                return RedirectToAction("EditBook", "BookAuthorClassification", new { id = bac.Book.ISBN });
            }
        }
    }
}