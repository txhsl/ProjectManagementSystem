using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace ProjectManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ProjectManagementSystemControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}