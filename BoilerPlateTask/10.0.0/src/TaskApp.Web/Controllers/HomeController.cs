using Microsoft.AspNetCore.Mvc;

namespace TaskApp.Web.Controllers
{
    public class HomeController : TaskAppControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}