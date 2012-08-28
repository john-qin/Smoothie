using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;

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



        public Entities.Smoothie Get(string id)
        {
            throw new NotImplementedException();
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }


        public int Save(Entities.Smoothie item)
        {
            using (var conn = OpenConnection())
            {
                string query = "";

                if (item.UserId == 0)
                {
                    query = "INSERT INTO dbo.Smoothie( Name, CreatedDate, Status ) values ";
                    query = query + " (@Name, @CreatedDate, @Status ) ";
                }
                else
                {
                    query = "INSERT INTO dbo.Smoothie( Name, CreatedDate, Status, UserId ) values ";
                    query = query + " (@Name, @CreatedDate, @Status, @UserId) ";
                }

                var parameters = new
                                     {
                                         Name = item.Name,
                                         CreatedDate = DateTime.Now,
                                         Status = SmoothieStatus.Approved,
                                         UserId = item.UserId
                                     };

                conn.Execute(query, parameters);
                SetIdentity<int>(conn, id => item.Id = id);
                return item.Id;

            }

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



        public void AddIngredients(string query, int smoothieId)
        {
            using (var conn = OpenConnection())
            {
                conn.Execute("AddSmoothieIngredients", new { query = query, smoothieId = smoothieId }, commandType: CommandType.StoredProcedure);
            }
        }


    }
}
