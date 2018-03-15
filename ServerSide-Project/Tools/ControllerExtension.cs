using Service.Models;
using System;
using System.Web.Mvc;

namespace ServerSide_Project.Tools
{
    public class ControllerExtension : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is AuthorizationException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction("Login", "Admin", new { returnBackTo = Request.Url });
            }
        }

        protected bool AuthorizeAndRedirect(Rank rank = Rank.Editor)
        {
            if (Session["authentication"] == null || (Rank)Session["level"] < rank)
            {
                throw new AuthorizationException();
            }
            return true;
        }
    }

    public class AuthorizationException : Exception { }
}