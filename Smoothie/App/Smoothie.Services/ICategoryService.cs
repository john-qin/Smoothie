using System.Collections.Generic;
using Smoothie.Domain.Entities;

namespace Smoothie.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        int Save(Category item);
        Category GetCategory(int id);
    }
}