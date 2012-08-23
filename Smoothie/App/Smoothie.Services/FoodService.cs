using System.Collections.Generic;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Repositories;

namespace Smoothie.Services
{
    public class FoodService : IFoodService
    {

        private readonly IFoodRepository _foodRepository;

        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public ActionConfirmation<IEnumerable<Food>> GetFoodList(string group, int page, int status)
        {
            var foodList = _foodRepository.GetFoodList(group, page, status, 25);
         
            var confirmation = new ActionConfirmation<IEnumerable<Food>>
             {
                 WasSuccessful = true,
                 Message = "",
                 Value = foodList
             };

            return confirmation;
        }

    
        public ActionConfirmation<IEnumerable<FoodGroupDto>> GetFoodGroups()
        {
            var foodGroups = _foodRepository.GetFoodGroups();
            var confirmation = new ActionConfirmation<IEnumerable<FoodGroupDto>>
                                   {
                                       WasSuccessful = true,
                                       Message =  "",
                                       Value = foodGroups
                                   };

            return confirmation;
        }


        public int TotalItemCount(string group, int status)
        {
            return _foodRepository.TotalItemCount(group, status);
        }


        public Food GetFood(string id)
        {
            return _foodRepository.Get(id);
        }


        public IEnumerable<Category> GetCategories()
        {
            return _foodRepository.GetCategories();
        }


        public void Update(Food food)
        {
            _foodRepository.Update(food);
        }
    }
}
