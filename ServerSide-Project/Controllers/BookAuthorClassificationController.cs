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
        public ActionResult CreateBookPost(BookAuthorClassification bac)
        {
            Book book = new Book(){ Title = bac.Book.Title, ISBN = bac.Book.ISBN, Pages = bac.Book.Pages, PublicationYear = bac.Book.PublicationYear,
                publicationinfo = bac.Book.publicationinfo, SignId = 78/*bac.Classifications.First().SignId,
                BookClassification = bac.Classifications.First(), Authors = bac.Authors*/};
            return RedirectToAction("CreateBook", "Book", book);
        }
    }
}