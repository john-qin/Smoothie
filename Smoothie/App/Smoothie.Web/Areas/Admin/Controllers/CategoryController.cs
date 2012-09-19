using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels.Admin;
using Smoothie.Services;
using Smoothie.Web.Infrastructure.Filters;

namespace Smoothie.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RequirePermission(RoleType.Administrator)]
    public partial class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public virtual ActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }


        public virtual ActionResult Add()
        {
            var category = new EditCategoryViewModel { Category = new Category() };
            return View("Edit", category);
        }


        public virtual ActionResult Edit(int id)
        {
            var category = _categoryService.GetCategory(id);

            var model = new EditCategoryViewModel { Category = category };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(EditCategoryViewModel model)
        {

            if (string.IsNullOrWhiteSpace(model.Category.Name))
            {
                ModelState.AddModelError("Category.Name", "Name is required");
            }

            if (model.Category.ReOrder < 1)
            {
                ModelState.AddModelError("Category.ReOrder", "ReOrder Number must be bigger than 0");
            }

            if (ModelState.IsValid)
            {
                var result = _categoryService.Save(model.Category);

                if (result > 0)
                    return RedirectToAction(MVC.Admin.Category.Index());
            }

            return View("Edit", model);
        }

    }
}
