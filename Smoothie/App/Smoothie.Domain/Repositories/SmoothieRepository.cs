using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Smoothie.Domain.Dto;
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
                const string query = "SELECT Id, Name , ReOrder " +
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



        public int AddIngredients(string query, int smoothieId, DateTime createdDate, int status, int userId)
        {
            using (var conn = OpenConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Query", query);
                parameter.Add("@SmoothieId", smoothieId);
                parameter.Add("@CreatedDate", createdDate);
                parameter.Add("@Status", status);
                parameter.Add("@UserId", userId);

                parameter.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


                conn.Execute("AddSmoothieIngredients", parameter, commandType: CommandType.StoredProcedure);

                return parameter.Get<int>("@Result");
            }
        }




        public IEnumerable<SmoothieIngredientsDto> GetSmoothieIngredients(int id)
        {
            using (var conn = OpenConnection())
            {
                var query = new StringBuilder();
                query.Append("SELECT si.smoothieid, ");
                query.Append("       si.quantity, ");
                query.Append("       si.ndb_no, ");
                query.Append("       f.energ_kcal AS Calories, ");
                query.Append("       f.lipid_tot  AS TotalFat, ");
                query.Append("       f.fa_sat     AS Saturated, ");
                query.Append("       f.fa_poly    AS Polyunsaturated, ");
                query.Append("       f.fa_mono    AS Monounsaturated, ");
                query.Append("       f.cholestrl  AS Cholesterol, ");
                query.Append("       f.fiber_td   AS TotalCarbs, ");
                query.Append("       f.fiber_td   AS DietaryFiber, ");
                query.Append("       f.sugar_tot  AS Sugars, ");
                query.Append("       f.protein    AS Protein, ");
                query.Append("       f.calcium, ");
                query.Append("       f.iron, ");
                query.Append("       f.magnesium, ");
                query.Append("       f.phosphorus, ");
                query.Append("       f.potassium, ");
                query.Append("       f.sodium, ");
                query.Append("       f.zinc, ");
                query.Append("       f.vit_a_iu   AS VitaminA, ");
                query.Append("       f.vit_c      AS VitaminC, ");
                query.Append("       f.vit_d      AS VitaminD, ");
                query.Append("       f.vit_e      AS VitaminE, ");
                query.Append("       thiamin, ");
                query.Append("       riboflavin, ");
                query.Append("       niacin, ");
                query.Append("       f.vit_b6     AS VitaminB6, ");
                query.Append("       f.folate_tot AS Folate, ");
                query.Append("       f.vit_b12    AS VitaminB12, ");
                query.Append("       name, ");
                query.Append("       [image], ");
                query.Append("       GmWt_3, ");
                query.Append("       GmWt_Desc3 ");
                query.Append("FROM   dbo.foodabbrev AS f ");
                query.Append("       INNER JOIN dbo.smoothieingredients AS si ");
                query.Append("               ON f.ndb_no = si.ndb_no ");
                query.Append("WHERE  si.smoothieid = @Id ");

                var parameters = new
                {
                    Id = id
                };
                return conn.Query<SmoothieIngredientsDto>(query.ToString(), parameters);
            }
        }


        public IEnumerable<Food> GetIngredients(string term)
        {
            using (IDbConnection conn = OpenConnection())
            {

                var parameters = new
                {
                    Term = term
                };
                return conn.Query<Food>("SearchIngredients", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
