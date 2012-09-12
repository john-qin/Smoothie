using System;
using System.Collections.Generic;
using System.Linq;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Repositories;
using Smoothie.Domain.ViewModels;

namespace Smoothie.Services
{
    public class SmoothieService : ISmoothieService
    {
        private readonly ISmoothieRepository _smoothieRepository;

        public SmoothieService(ISmoothieRepository smoothieRepository)
        {
            _smoothieRepository = smoothieRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _smoothieRepository.GetCategories();
        }


        public IEnumerable<Food> GetIngredients(int category)
        {
            return _smoothieRepository.GetIngredients(category);
        }


        public int AddSmoothie(Domain.Entities.Smoothie item)
        {
            return _smoothieRepository.Save(item);
        }


        public int AddIngredients(string query, int smoothieId, DateTime createdDate, int status, int userId)
        {
           return _smoothieRepository.AddIngredients(query, smoothieId, createdDate, status, userId);
        }


        public IEnumerable<SmoothieIngredientsDto> GetSmoothieIngredients(int id)
        {
            return _smoothieRepository.GetSmoothieIngredients(id);

        }

        public SmoothieSummaryViewModel GetSmoothieSummary(int id)
        {
            var ingredients = _smoothieRepository.GetSmoothieIngredients(id);
            var result = new SmoothieSummaryViewModel
                             {
                                 Ingredients = ingredients,
                                 SmoothieId = id,
                                 Calories = (ingredients.Sum(x => x.Calories * x.GmWt_3 * x.Quantity / 100)),
                                 TotalFat = (ingredients.Sum(x => x.TotalFat * x.GmWt_3 * x.Quantity / 100)),
                                 Saturated = (ingredients.Sum(x => x.Saturated * x.GmWt_3 * x.Quantity / 100)),
                                 Polyunsaturated = (ingredients.Sum(x => x.Polyunsaturated * x.GmWt_3 * x.Quantity / 100)),
                                 Monounsaturated = (ingredients.Sum(x => x.Monounsaturated * x.GmWt_3 * x.Quantity / 100)),
                                 Cholesterol = (ingredients.Sum(x => x.Cholesterol * x.GmWt_3 * x.Quantity / 100)),
                                 TotalCarbs = (ingredients.Sum(x => x.TotalCarbs * x.GmWt_3 * x.Quantity / 100)),
                                 DietaryFiber = (ingredients.Sum(x => x.DietaryFiber * x.GmWt_3 * x.Quantity / 100)),
                                 Sugars = (ingredients.Sum(x => x.Sugars * x.GmWt_3 * x.Quantity / 100)),
                                 Protein = (ingredients.Sum(x => x.Protein * x.GmWt_3 * x.Quantity / 100)),

                                 Calcium = (ingredients.Sum(x => x.Calcium * x.GmWt_3 * x.Quantity / 100)),
                                 Iron = (ingredients.Sum(x => x.Iron * x.GmWt_3 * x.Quantity / 100)),
                                 Magnesium = (ingredients.Sum(x => x.Magnesium * x.GmWt_3 * x.Quantity / 100)),
                                 Phosphorus = (ingredients.Sum(x => x.Phosphorus * x.GmWt_3 * x.Quantity / 100)),
                                 Potassium = (ingredients.Sum(x => x.Potassium * x.GmWt_3 * x.Quantity / 100)),
                                 Sodium = (ingredients.Sum(x => x.Sodium * x.GmWt_3 * x.Quantity / 100)),
                                 Zinc = (ingredients.Sum(x => x.Zinc * x.GmWt_3 * x.Quantity / 100)),


                                 VitaminA = (ingredients.Sum(x => x.VitaminA * x.GmWt_3 * x.Quantity / 100)),
                                 VitaminC = (ingredients.Sum(x => x.VitaminC * x.GmWt_3 * x.Quantity / 100)),
                                 VitaminD = (ingredients.Sum(x => x.VitaminD * x.GmWt_3 * x.Quantity / 100)),
                                 VitaminE = (ingredients.Sum(x => x.VitaminE * x.GmWt_3 * x.Quantity / 100)),
                                 Thiamin = (ingredients.Sum(x => x.Thiamin * x.GmWt_3 * x.Quantity / 100)),
                                 Riboflavin = (ingredients.Sum(x => x.Riboflavin * x.GmWt_3 * x.Quantity / 100)),
                                 Niacin = (ingredients.Sum(x => x.Niacin * x.GmWt_3 * x.Quantity / 100)),
                                 VitaminB6 = (ingredients.Sum(x => x.VitaminB6 * x.GmWt_3 * x.Quantity / 100)),
                                 Folate = (ingredients.Sum(x => x.Folate * x.GmWt_3 * x.Quantity / 100)),
                                 VitaminB12 = (ingredients.Sum(x => x.VitaminB12 * x.GmWt_3 * x.Quantity / 100))
                             };

            return result;
        }


        public IEnumerable<Food> GetIngredients(string term)
        {
            return _smoothieRepository.GetIngredients(term);
        }
    }
}
