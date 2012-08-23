using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.Repositories
{
    public class SmoothieRepository : BaseRepository, ISmoothieRepository
    {
        public IEnumerable<Category> GetCategories()
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "SELECT Name , ReOrder " +
                                     "FROM    dbo.Category " +
                                     "ORDER BY ReOrder";
                return conn.Query<Category>(query);
            }
        }



        public Food Get(string id)
        {
            throw new NotImplementedException();
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }


        public int Save(Food item)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Food> GetIngredients(int category)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "SELECT * " +
                                     "FROM    dbo.FoodAbbrev " +
                                     "WHERE GroupId = @Category";

                var parameters = new
                {
                    Category = category
                };
                return conn.Query<Food>(query, parameters);
            }
        }
    }
}
