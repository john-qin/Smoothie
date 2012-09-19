using System.Collections.Generic;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        IEnumerable<Category> GetAll();

    }
}