using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.ViewModels.Admin
{
    public class AdminFoodViewModel
    {
        public IEnumerable<Food> FoodList { get; set; }
        public IEnumerable<FoodGroupDto> FoodGroups { get; set; }
        public PagingDto Paging { get; set; }
    }
}
