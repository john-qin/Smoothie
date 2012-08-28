using System.Collections.Generic;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.ViewModels
{
    public class SmoothieSummaryViewModel
    {
        public IEnumerable<SmoothieIngredients> Ingredientses { get; set; }


    }
}
