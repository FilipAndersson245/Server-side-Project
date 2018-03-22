using Service.Models;
using System;
using System.Web.Mvc;

namespace ServerSide_Project.Tools
{
    /// <summary>
    /// Adds functionality to the controllers.
    /// </summary>
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

        /// <summary>
        /// Makes sure that the user is signed in as the specified rank before proceeding.
        /// </summary>
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