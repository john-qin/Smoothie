using System.Collections.Generic;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;

namespace Smoothie.Services
{
    public interface IFoodService
    {
        ActionConfirmation<IEnumerable<Food>> GetFoodList(string group, int page, int status);
        ActionConfirmation<IEnumerable<FoodGroupDto>> GetFoodGroups();
        int TotalItemCount(string group, int status);
        Food GetFood(string id);
        IEnumerable<Category> GetCategories();
        void Update(Food food);
    }
}
