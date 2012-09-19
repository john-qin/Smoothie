using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels.Admin;
using Smoothie.Services;
using Smoothie.Web.Infrastructure;
using Smoothie.Web.Infrastructure.Filters;

namespace Smoothie.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RequirePermission(RoleType.Administrator)]
    public partial class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IMappingService _mappingService;

        public FoodController(IFoodService foodService, IMappingService mappingService)
        {
            if (foodService == null)
                throw new ArgumentNullException("foodService");

            if (mappingService == null)
                throw new ArgumentNullException("mappingService");

            _foodService = foodService;
            _mappingService = mappingService;
        }

        [GET("Food/{status:regex(^Approved|Pending|Deleted$)}")]
        public virtual ActionResult Index(string group, string page, string status)
        {
            if (String.IsNullOrWhiteSpace(group)) group = "";

            FoodStatus foodStatus;
            Enum.TryParse(status, true, out foodStatus);

            int pageNum = page.Integer(1);
            int pageSize = System.Configuration.ConfigurationManager.AppSettings["PageSize"].Integer(25);

            var foodList = _foodService.GetFoodList(group, pageNum, (int)foodStatus);
            var foodGroups = _foodService.GetFoodGroups();
            var totalFood = _foodService.TotalItemCount(group, (int)foodStatus);

            var model = new AdminFoodViewModel
                            {
                                FoodList = foodList.Value,
                                FoodGroups = foodGroups.Value,
                                Paging = new PagingDto
                                             {
                                                 ItemPerPage = pageSize,
                                                 CurrentPage = pageNum,
                                                 TotalItems = totalFood
                                             }
                            };

            var currentGroup = foodGroups.Value.FirstOrDefault(x => x.FdGrp_CD == @group);

            ViewBag.GroupName = currentGroup == null ? "All Products" : currentGroup.GroupDesc;
            ViewBag.GroupId = currentGroup == null ? "" : currentGroup.FdGrp_CD;


            return View(model);
        }


        public virtual ActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id", "Id cannot be null");


 


            var food = _foodService.GetFood(id);
            var categories = _foodService.GetCategories();
            var model = new EditFoodViewModel
                            {
                                Food = food,
                                Categories = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList() 
                            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(EditFoodViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Food.Name))
            {
                ModelState.AddModelError("Food.Name", "Food Name is required");
            }


            if (string.IsNullOrWhiteSpace(model.Food.Image))
            {
                ModelState.AddModelError("Food.Image", "Image is required");
            }

            if (model.Food.GroupId <= 0)
            {
                ModelState.AddModelError("Food.GroupId", "Select a category");
            }

            if (model.Food.GmWt_3 < 0)
            {
                ModelState.AddModelError("Food.GmWt_3", "Required field");
            }

            if (string.IsNullOrWhiteSpace(model.Food.GmWt_Desc3))
            {
                ModelState.AddModelError("Food.GmWt_Desc3", "Required field");
            }

            if(ModelState.IsValid)
            {
                _foodService.Update(model.Food);
                return RedirectToAction(MVC.Admin.Food.Index());
            }

           

            var categories = _foodService.GetCategories();
            model.Categories = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(); 

            return View(model);
        }

    }
}
