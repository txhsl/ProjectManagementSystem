using System.Web.Mvc;

namespace ProjectManagementSystem.Web.Controllers
{
    public class AboutController : ProjectManagementSystemControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}