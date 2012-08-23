
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;

namespace Smoothie.Domain.ViewModels.Admin
{
    public class EditFoodViewModel
    {
        public Food Food { get; set; }
        public IEnumerable<EnumItem> StatusList { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public EditFoodViewModel()
        {
            StatusList = from FoodStatus e in Enum.GetValues(typeof(FoodStatus))
                         select new EnumItem
                                    {
                                        Id = ((int)e).ToString(),
                                        Value = e.ToString()
                                    };
        }
    }
}
