using System.Web.Mvc;

namespace ServerSide_Project.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Home");
        }
    }
}