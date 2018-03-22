using System.Web.Mvc;

namespace ServerSide_Project.Tools
{
    /// <summary>
    /// Set TempData with the values from the ModelState.
    /// This stores the model state for further use.
    /// </summary>
    public class SetTempDataModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            filterContext.Controller.TempData["ModelState"] =
               filterContext.Controller.ViewData.ModelState;
        }
    }

    /// <summary>
    /// Restores the ModelState to a saved temporary version 
    /// </summary>
    public class RestoreModelStateFromTempDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.Controller.TempData.ContainsKey("ModelState"))
            {
                filterContext.Controller.ViewData.ModelState.Merge(
                    (ModelStateDictionary)filterContext.Controller.TempData["ModelState"]);
            }
        }
    }
}