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

        [HttpGet]
        public ActionResult BrowseAllBooks(int page = 1)
        {
            BookManager bookManager = new BookManager();
            return View("BrowseAllBooks", bookManager.GetAllBooks(page, ITEMS_PER_PAGE));
        }

        [HttpGet]
        public ActionResult ListBookDetails(string id)
        {
            BookManager bookManager = new BookManager();
            return View("ListBookDetails", bookManager.GetBookFromIsbn(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookPost(string id)
        {
            AuthorizeAndRedirect();
            BookManager bookManager = new BookManager();
            if (bookManager.DeleteBook(id))
                return RedirectToAction("BrowseAllBooks", "Book", null);
            else
                return RedirectToAction("BrowseAllBooks", "Book", null);
        }

        [HttpGet]
        public ActionResult DeleteBook(string id)
        {
            BookManager manager = new BookManager();
            return View("DeleteBook", manager.GetBookFromIsbn(id));
        }

        [HttpGet]
        public ActionResult SearchBooks(string search, int page = 1, params int[] classifications)
        {
            BookManager bookManager = new BookManager();
            return View("BrowseSearchedBooks", bookManager.SearchBooks(search, page, ITEMS_PER_PAGE, classifications));
        }

        [HttpGet]
        public JsonResult SEARCH(string search)
        {
            var a = new BookManager().SearchBooks(new Search() { SearchQuery = search, SelectedClassifications = new List<int>() { 1, 2, 3, 4, 5 } }, 1, 20);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(a);
            //return new JsonpResult(json);
            //JavaScriptSerializer()
            return Json(a, "application/javascript", System.Text.UTF8Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }






    public class JsonpResult : JsonResult
    {
        object Data = null;

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