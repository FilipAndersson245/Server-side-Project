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
        [ActionName("CreateBook")]
        public ActionResult CreateBookPost(BookAuthorClassification bac, string[] authorChecklist, int classificationRadio)
        {
            AuthorManager authorManager = new AuthorManager();
            ClassificationManager classificationManager = new ClassificationManager();
            BookManager bookManager = new BookManager();
            Classification classification = classificationManager.GetClassificationFromID(classificationRadio);
            List<Author> authorList = new List<Author>();
            foreach(var aID in authorChecklist)
            {
                authorList.Add(authorManager.GetAuthorFromID(Convert.ToInt32(aID)));
            }
            Book book = new Book(){ Title = bac.Book.Title, ISBN = bac.Book.ISBN, Pages = bac.Book.Pages, PublicationYear = bac.Book.PublicationYear,
                publicationinfo = bac.Book.publicationinfo, SignId = classification.SignId,
                BookClassification = classification, Authors = authorList};
            return View("ListBookDetails", bookManager.CreateBook(book));
        }
    }
}