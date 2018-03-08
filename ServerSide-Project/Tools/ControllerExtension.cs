using Service.Models;
using System;
using System.Web.Mvc;

namespace ServerSide_Project.Tools
{
    public class ControllerExtension : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is ValidationException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction("Login", "Admin", new { returnBackTo = Request.Url });
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