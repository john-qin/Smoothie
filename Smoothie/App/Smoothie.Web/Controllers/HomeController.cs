using System;
using System.Text;
using System.Web.Mvc;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Enums;
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




        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(FormCollection form, UserDataDto userData, int category = 1)
        {
            string[] qtyList = Request.Form.GetValues("qty");
            string[] idList = Request.Form.GetValues("targetName");

            if (qtyList == null || idList == null)
            {
                ModelState.AddModelError("", "Blender is empty");
            }

            if (ModelState.IsValid)
            {
                var newSmoothie = new Smoothie.Domain.Entities.Smoothie
                                      {
                                          Id = 0,
                                          Name = "",
                                          CreatedDate = DateTime.Now,
                                          Status = SmoothieStatus.Approved,
                                          UserId = (userData == null) ? 0 : userData.Id
                                      };
                var smoothieId = _smoothieService.AddSmoothie(newSmoothie);

                var query = new StringBuilder();
                query.Append("INSERT INTO dbo.SmoothieIngredients ( NDB_No, SmoothieId, Quantity ) VALUES ");

                for (int i = 0; i < idList.Length; i++)
                {
                    var quantity = 1;
                    int.TryParse(qtyList[i], out quantity);

                    if (quantity == 0) quantity = 1;

                    if (i > 0) query.Append(" , ");
                    query.Append(string.Format("( N'{0}', {1}, {2})", idList[i], smoothieId, quantity));
                }

                _smoothieService.AddIngredients(query.ToString(), 0);


                return RedirectToAction(MVC.Home.About());

            }
            else
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
        }




        public virtual ActionResult Summary(int id)
        {

            return PartialView();

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
