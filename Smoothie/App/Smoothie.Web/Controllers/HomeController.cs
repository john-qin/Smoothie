using System.Web.Mvc;
using Smoothie.Domain.Dto;
using Smoothie.Domain.ViewModels;
using Smoothie.Services;

namespace Smoothie.Web.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly ISmoothieService _smoothieService;

        public HomeController(ISmoothieService smoothieService)
        {
            _smoothieService = smoothieService;
        }

        public virtual ActionResult Index(UserDataDto userData, int category = 1)
        {
            var categories = _smoothieService.GetCategories();
            var ingredients = _smoothieService.GetIngredients(category);

            var model = new MakeSmoothieViewModel
                            {
                                Categories = categories,
                                Ingredients = ingredients
                            };

            return View(model);
        }

        public virtual ActionResult About()
        {
            return View();
        }


        [ChildActionOnly]
        public virtual PartialViewResult UserProfile(UserDataDto userData)
        {
            return PartialView("_UserProfile", userData);
        }


    }
}
