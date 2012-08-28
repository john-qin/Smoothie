using System.Collections.Generic;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.Repositories
{
    public interface ISmoothieRepository : IRepository<Entities.Smoothie, string>
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Food> GetIngredients(int category);
        void AddIngredients(string query, int smoothieId);
    }
}
