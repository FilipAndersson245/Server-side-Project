using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Service.Models;
using Service.Managers;
using ServerSide_Project.Tools;

namespace ServerSide_Project.Controllers
{
    public class BookAuthorClassificationController : Controller
    {

        [HttpGet]
        public ActionResult CreateBook()
        {
            BookAuthorClassificationManager bacManager = new BookAuthorClassificationManager();
            var bac = bacManager.Setup();
            return View("CreateBook", bac);
        }

        [HttpPost]
        public ActionResult CreateBook(BookAuthorClassification bac, string[] authorChecklist, int classificationRadio)
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
            Book newBook = bookManager.CreateBook(book);
            return RedirectToAction("ListBookDetails", "Book", new { id = newBook.ISBN });
        }

        [HttpGet]
        public ActionResult EditBook(string id)
        {
            BookManager bookManager = new BookManager();
            BookAuthorClassificationManager bacManager = new BookAuthorClassificationManager();
            var bac = bacManager.Setup();
            bac.Book = bookManager.GetBookFromIsbn(id);
            return View("EditBook", bac);
        }

        [HttpPost]
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
            return RedirectToAction("ListBookDetailsFromBook", "Book", bookManager.EditBook(book).ISBN);
        }

    }
}