using ServerSide_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
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

            return RedirectToAction("ListBooks", "Books", null); //maybe to the created book instead of list
        }

        [HttpGet]
        public ActionResult ListBooks()
        {
            Repository repo = (Repository)Session["repo"];
            return View("ListBooks", repo.BookList);

        }

        [HttpGet]
        public ActionResult ListBooksByAuthor(Author author)
        {
            return View("ListBooks", author.BookList);
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            Repository repo = (Repository)Session["repo"];
            return View("ListBookDetails", repo.BookList.FirstOrDefault(x => x.ISBN == id));
        }

        [HttpGet]
        public ActionResult EditBook(string id)
        {
            //TempData.Add("ISBN", id);
            var repo = Session["repo"] as Repository;
            return View("EditBook", repo.BookList.Find(x => x.ISBN == id)); // ret a book with ISBN equal to id
        }


        [HttpPost]
        public ActionResult UpdateBook(Book book)
        {

            if (ModelState.IsValid) //validate the data
            {
                //int a = 5;
            }
            
            string oldISBN = (string)TempData["ISBN"];

            var repo = Session["repo"] as Repository;
            repo.BookList.Where(d => d.ISBN == oldISBN).First().SetBook(book);

            return RedirectToAction("ListBooks", "Books");
        }

        [HttpGet]
        public ActionResult DeleteBook(string id)
        {
            Repository repo = (Repository)Session["repo"];
            repo.BookList.RemoveAll(x => x.ISBN == id);
            return RedirectToAction("ListBooks", "Books", null);
        }
    }
}