using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;

namespace Smoothie.Services
{
    public interface ISmoothieService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Food> GetIngredients(int category);
        int AddSmoothie(Domain.Entities.Smoothie item);
        void AddIngredients(string query, int smoothieId);
    }
}
