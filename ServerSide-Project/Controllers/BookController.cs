using ServerSide_Project.Tools;
using Service.Managers;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace ServerSide_Project.Controllers
{
    public class BookController : ControllerExtension
    {
        public const int ITEMS_PER_PAGE = 10;
        private BookManager _BookManager { get; } = new BookManager();

        [HttpGet]
        public ActionResult BrowseAllBooks(int page = 1)
        {
            return View("BrowseAllBooks", _BookManager.GetAllBooks(page, ITEMS_PER_PAGE));
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            return View("ListBookDetails", _BookManager.GetBookFromIsbn(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookPost(string id)
        {
            AuthorizeAndRedirect();
            if (_BookManager.DeleteBook(id)) //why? vvvvvvvv
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult DeleteBook(string id)
        {
            return View("DeleteBook", _BookManager.GetBookFromIsbn(id));
        }

        [HttpGet]
        public ActionResult SearchBooks(string search, int page = 1, params int[] classifications)
        {
            return View("BrowseSearchedBooks", _BookManager.SearchBooks(search, page, ITEMS_PER_PAGE, classifications));
        }

        [HttpGet]
        public JsonResult SEARCH(string search)
        {
            var searchedBookList = _BookManager.GetSearchedBooksToList(new Search() { SearchQuery = search });
            return new JsonpResult(searchedBookList);
        }
    }

    public class JsonpResult : JsonResult
    {
        private new object Data = null;

        public JsonpResult()
        {
        }

        public JsonpResult(object Data)
        {
            this.Data = Data;
        }

        public override void ExecuteResult(ControllerContext ControllerContext)
        {
            if (ControllerContext != null)
            {
                HttpResponseBase Response = ControllerContext.HttpContext.Response;
                HttpRequestBase Request = ControllerContext.HttpContext.Request;

                string callbackfunction = Request["callback"];
                if (string.IsNullOrEmpty(callbackfunction))
                {
                    throw new Exception("Callback function name must be provided in the request!");
                }
                Response.ContentType = "application/x-javascript";
                if (Data != null)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Response.Write(string.Format("{0}({1});", callbackfunction, serializer.Serialize(Data)));
                }
            }
        }
    }
}