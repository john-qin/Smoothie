using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels;
using Smoothie.Web.Infrastructure.Filters;

namespace Smoothie.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public partial class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

        [RequirePermission(RoleType.Administrator)]
        [GET("Index")]
        public virtual ActionResult Index()
        {
            return View();
        }

        
        [GET("Login")]
        public virtual ActionResult Login()
        {
            return View(new UserLoginViewModel());
        }

    }
}
