using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;

namespace Smoothie.Domain.ViewModels.Admin
{
    public class EditCategoryViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<EnumItem> StatusList { get; set; }

        public EditCategoryViewModel()
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
