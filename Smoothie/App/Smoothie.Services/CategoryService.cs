using System.Collections.Generic;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Repositories;

namespace Smoothie.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Domain.Entities.Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public int Save(Category item)
        {
            return _categoryRepository.Save(item);
        }


        public Category GetCategory(int id)
        {
            return _categoryRepository.Get(id);
        }
    }
}
