using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;

namespace ServerSide_Project.Tool
{
    public class ControllerExtension : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {

            if (filterContext.Exception is ValidationException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction("Login", "Admin", new { returnBackTo = Request.RawUrl });
            }
        }

        protected bool ValidateAndRedirect(Rank rank = Rank.Admin)
        {
            if (Session["authentication"] == null || (Rank)Session["level"] < rank)
            {
                throw new ValidationException();
            }
            return true;
        }
    }

    public class ValidationException : Exception { }
}