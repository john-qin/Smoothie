using System;
using System.Collections.Generic;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.Repositories
{
    public interface ISmoothieRepository : IRepository<Entities.Smoothie, string>
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Food> GetIngredients(int category);
        IEnumerable<Food> GetIngredients(string term);
        int AddIngredients(string query, int smoothieId, DateTime createdDate, int status, int userId);
        IEnumerable<SmoothieIngredientsDto> GetSmoothieIngredients(int id);
    }
}
