using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class CreateController : Controller
    {
        // GET: Create
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateBook()
        {
            Repository repo = (Repository)Session["repo"];

            return View("CreateBook", new AuthorAndGenre() { AuthorList = repo.AuthorList, GenreList = new List<Genre>() { new Genre() { Art = null, Name = "Fantasy" } } });
        }

        [HttpPost]
        [ActionName("CreateBook")]              // cannot have equal names so using this to emulate same same
        public ActionResult CreateBookPost(Book book)    //
        {
            if (ModelState.IsValid)
            {
                //valid book!
            }
            Repository repo = (Repository)Session["repo"];
            book.BookAuthor = new Author { ID = "11", FirstName = "Test", LastName = "Author", BirthYear = 2000 };
            book.BookGenre = new Genre { Art = null, Name = "TestGenre" };
            repo.BookList.Add(book);

            return RedirectToAction("ListBooks", "List", null); //maybe to the created book instead of list
        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View("CreateAdmin");
        }

        [HttpPost]
        public ActionResult CreateAdmin(Admin admin)
        {
            Repository repo = (Repository)Session["repo"];
            repo.AdminList.Add(admin);
            return RedirectToAction("GetAdminView", "Update", null);
        }

        [HttpGet]
        public ActionResult CreateAuthor()
        {
            return View("CreateAuthor");
        }

        [HttpPost]
        public ActionResult CreateAuthor(Author author)
        {
            Repository repo = (Repository)Session["repo"];
            repo.AuthorList.Add(author);
            return RedirectToAction("ListAuthors", "List", null);
        }
    }
}