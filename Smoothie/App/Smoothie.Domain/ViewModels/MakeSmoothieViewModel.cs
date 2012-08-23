using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.ViewModels
{
    public class MakeSmoothieViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Food> Ingredients { get; set; }

    }
}
