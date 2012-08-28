using System;
using System.Collections.Generic;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Repositories;

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


        public void AddIngredients(string query, int smoothieId)
        {
            _smoothieRepository.AddIngredients(query, smoothieId);
        }
    }
}
