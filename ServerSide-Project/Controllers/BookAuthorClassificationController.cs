using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class BookAuthorClassificationController : ControllerExtension
    {
        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult CreateBook()
        {
            BookAuthorClassificationManager bacManager = new BookAuthorClassificationManager();
            var bac = bacManager.Setup();
            return View("CreateBook", bac);
        }

        [HttpPost]
        [SetTempDataModelState]
        public ActionResult CreateBook(BookAuthorClassification bac, string[] authorChecklist, int? classificationRadio) //Strukturera om till servicelagret och lös classificationRadio null
        {
            ValidateAndRedirect();
            BookManager bookManager = new BookManager();
            var bookTuple = bookManager.CreateBook(bac, authorChecklist, classificationRadio);
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
            BookManager bookManager = new BookManager();
            BookAuthorClassificationManager bacManager = new BookAuthorClassificationManager();
            var bac = bacManager.Setup();
            bac.Book = bookManager.GetBookFromIsbn(id);
            return View("EditBook", bac);
        }

        [HttpPost]
        [SetTempDataModelState]
        public ActionResult EditBook(BookAuthorClassification bac, string[] authorChecklist, int classificationRadio)
        {
            AuthorManager authorManager = new AuthorManager();
            ClassificationManager classificationManager = new ClassificationManager();
            BookManager bookManager = new BookManager();
            Classification classification = classificationManager.GetClassificationFromID(classificationRadio);
            List<Author> authorList = new List<Author>();
            foreach (var aID in authorChecklist)
            {
                authorList.Add(authorManager.GetAuthorFromID(Convert.ToInt32(aID)));
            }
            Book book = new Book()
            {
                Title = bac.Book.Title,
                ISBN = bac.Book.ISBN,
                Pages = bac.Book.Pages,
                PublicationYear = bac.Book.PublicationYear,
                Publicationinfo = bac.Book.Publicationinfo,
                SignId = classification.SignId,
                Classification = classification,
                Authors = authorList
            };
            ValidateAndRedirect();
            var bookTuple = bookManager.EditBook(book);
            if (bookTuple.Item2.IsValid)
                return RedirectToAction("ListBookDetails", "Book", new { id = bookTuple.Item1.ISBN });
            else
            {
                ValidationMessages.ConvertCodeToMsg(ModelState, bookTuple.Item2.ErrorDict);
                return RedirectToAction("EditBook", "BookAuthorClassification", new { id = book.ISBN });
            }
        }
    }
}