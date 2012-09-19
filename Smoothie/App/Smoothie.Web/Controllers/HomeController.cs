using System;
using System.Linq;
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


        public virtual ActionResult Summary(FormCollection form, UserDataDto userData, int smoothieId, int category = 1)
        {
            string[] qtyList = Request.Form.GetValues("qty");
            string[] idList = Request.Form.GetValues("targetName");

            if (qtyList != null && idList != null)
            {
                if (ModelState.IsValid)
                {
                    var createdDate = DateTime.Now;
                    var status = SmoothieStatus.Approved;
                    var userId = (userData == null) ? 0 : userData.Id;


                    var query = new StringBuilder();
                    query.Append("INSERT INTO dbo.SmoothieIngredients ( NDB_No, SmoothieId, Quantity ) VALUES ");

                    for (int i = 0; i < idList.Length; i++)
                    {
                        var quantity = 1;
                        int.TryParse(qtyList[i], out quantity);

                        if (quantity == 0) quantity = 1;

                        if (i > 0) query.Append(" , ");
                        query.Append(string.Format("( N'{0}', {1}, {2})", idList[i], (smoothieId > 0) ? smoothieId.ToString() : "sId", quantity));
                    }

                    var sId = _smoothieService.AddIngredients(query.ToString(), smoothieId, createdDate, (int)status, userId);
                    var summary = _smoothieService.GetSmoothieSummary(sId);
                    return PartialView(summary);
                }
            }

            return PartialView("EmptySummary");

            //var categories = _smoothieService.GetCategories();
            //var ingredients = _smoothieService.GetIngredients(category);

            //var model = new MakeSmoothieViewModel
            //{
            //    Categories = categories,
            //    Ingredients = ingredients
            //};

            //return View(model);

        }


        public virtual ActionResult Blender(int smoothieId)
        {
            var ingredients = _smoothieService.GetSmoothieIngredients(smoothieId);

            return PartialView(ingredients);
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


        public virtual ActionResult GetCategory(int id)
        {
            var ingredients = _smoothieService.GetIngredients(id);
            return PartialView(ingredients);
        }


        public virtual JsonResult SearchIngredients(string term)
        {
            var ingredients = _smoothieService.GetIngredients(term);

            var data = ingredients.Select(x => new {Id = x.NDB_No, Value = x.Name}).Take(25).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}
