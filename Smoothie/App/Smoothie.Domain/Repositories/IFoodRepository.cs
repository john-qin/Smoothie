using System.Collections.Generic;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.Repositories
{
    public interface IFoodRepository : IRepository<Food, string>
    {
        IEnumerable<Food> GetFoodList(string group, int page, int status, int itemPerPage);
        IEnumerable<FoodGroupDto> GetFoodGroups();
        int TotalItemCount(string group, int status);
        IEnumerable<Category> GetCategories();
        void Update(Food food);
    }
}
