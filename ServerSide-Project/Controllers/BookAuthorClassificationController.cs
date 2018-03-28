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
        private BookManager _Manager { get; } = new BookManager();
        private BookAuthorClassificationManager _BookAuthorClassificationManager { get; } = new BookAuthorClassificationManager();

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult CreateBook()
        {
            AuthorizeAndRedirect();
            BookAuthorClassification bac = _BookAuthorClassificationManager.Setup();
            return View("CreateBook", bac);
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(BookAuthorClassification bookAuthorClassification, string[] authorChecklist, int? classificationRadio)
        {
            AuthorizeAndRedirect();
            Tuple<Book, BookValidation> bookTuple = _Manager.CreateBook(bookAuthorClassification, authorChecklist, classificationRadio);
            if (bookTuple.Item2.IsValid)
            {
                ModelState.Clear();
                return RedirectToAction("ListBookDetails", "Book", new { id = bookTuple.Item1.ISBN });
            }
            ValidationMessages.ConvertCodeToMsg(ModelState, bookTuple.Item2.ErrorDict);
            return RedirectToAction("CreateBook", "BookAuthorClassification");
        }

        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult EditBook(string id)
        {
            AuthorizeAndRedirect();
            BookAuthorClassification bookAuthorClassification = _BookAuthorClassificationManager.Setup();
            bookAuthorClassification.Book = _Manager.GetBookFromIsbn(id);
            return View("EditBook", bookAuthorClassification);
        }

        [HttpPost]
        [SetTempDataModelState]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(BookAuthorClassification bookAuthorClassification, string[] authorChecklist, int classificationRadio)
        {
            AuthorizeAndRedirect();
            Tuple<Book, BookValidation> bookTuple = _Manager.EditBook(bookAuthorClassification, authorChecklist, classificationRadio);
            if (bookTuple.Item2.IsValid)
            {
                ModelState.Clear();
                return RedirectToAction("ListBookDetails", "Book", new { id = bookTuple.Item1.ISBN });
            }
            ValidationMessages.ConvertCodeToMsg(ModelState, bookTuple.Item2.ErrorDict);
            return RedirectToAction("EditBook", "BookAuthorClassification", new { id = bookAuthorClassification.Book.ISBN });
        }
    }
}