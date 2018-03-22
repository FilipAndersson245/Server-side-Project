using Service.Managers;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ServerSide_Project.Controllers
{

    public class ApiController : Controller
    {
        private BookManager Manager { get; } = new BookManager();

        [HttpGet]
        public JsonResult Search(string search)
        {
            var searchedBookList = Manager.GetSearchedBooksToList(new Search() { SearchQuery = search });
            return new JsonpResult(searchedBookList);
        }
    }

    /// <summary>
    /// Allows use of JSONP.
    /// </summary>
    public class JsonpResult : JsonResult
    {
        private new object Data = null;

        public JsonpResult()
        {
        }

        public JsonpResult(object data)
        {
            this.Data = data;
        }

        public override void ExecuteResult(ControllerContext controllerContext)
        {
            if (controllerContext != null)
            {
                HttpResponseBase Response = controllerContext.HttpContext.Response;
                HttpRequestBase Request = controllerContext.HttpContext.Request;
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