using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.ViewModels;

namespace Smoothie.Services
{
    public interface ISmoothieService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Food> GetIngredients(int category);
        int AddSmoothie(Domain.Entities.Smoothie item);
        int AddIngredients(string query, int smoothieId, DateTime createdDate, int status, int userId);
        SmoothieSummaryViewModel GetSmoothieSummary(int id);
        IEnumerable<SmoothieIngredientsDto> GetSmoothieIngredients(int id);
        IEnumerable<Food> GetIngredients(string term);
    }
}
