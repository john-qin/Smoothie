using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.Repositories
{
    public interface ISmoothieRepository : IRepository<Food, string>
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Food> GetIngredients(int category);

    }
}
