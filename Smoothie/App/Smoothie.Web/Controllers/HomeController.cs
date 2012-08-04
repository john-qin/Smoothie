using System.Web.Mvc;
using Smoothie.Domain.Dto;

namespace Smoothie.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index(UserDataDto userData)
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public virtual ActionResult About()
        {
            return View();
        }


        [ChildActionOnly]
        public virtual PartialViewResult UserProfile(UserDataDto userData)
        {
            return PartialView("_UserProfile",userData);
        }


    }
}
