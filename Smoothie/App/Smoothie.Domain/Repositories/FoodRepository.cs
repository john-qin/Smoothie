using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.Repositories
{
    public class FoodRepository : BaseRepository, IFoodRepository
    {
        public IEnumerable<Food> GetFoodList(string group, int page, int status = 0, int itemPerPage = 25)
        {
            using (IDbConnection conn = OpenConnection())
            {
                var query = new StringBuilder();
                query.Append("SELECT  * ");
                query.Append("FROM    ( SELECT    tbl.* , ");
                query.Append("                    ROW_NUMBER() OVER ( ORDER BY tbl.NDB_No ) rownum ");
                query.Append("          FROM      dbo.FoodAbbrev AS tbl ");
                if (status > 0)
                    query.Append("      WHERE Status = @Status");
                if (!string.IsNullOrWhiteSpace(group))
                {
                    if (status > 0)
                        query.Append(" AND ");
                    else
                        query.Append(" WHERE ");
                    query.Append("               GroupCd = @groupId ");
                }
             
                query.Append("        ) seq ");
                query.Append("WHERE   seq.rownum BETWEEN @x AND @y ");
                query.Append("ORDER BY seq.rownum ");


                var parameters = new
                                 {
                                     Status = status,
                                     groupId = group,
                                     x = (page - 1) * itemPerPage,
                                     y = page * itemPerPage
                                 };

                return conn.Query<Food>(query.ToString(), parameters);
            }
        }

        public IEnumerable<FoodGroupDto> GetFoodGroups()
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "SELECT FdGrp_CD , GroupDesc " +
                                     "FROM    dbo.FoodGroup " +
                                     "ORDER BY GroupDesc";
                return conn.Query<FoodGroupDto>(query);
            }
        }

        public Food Get(string id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "SELECT * " +
                                     "FROM    dbo.FoodAbbrev " +
                                     "WHERE NDB_No = @Id";

                var parameters = new
               {
                   Id = id
               };
                return conn.Query<Food>(query, parameters).SingleOrDefault();
            }
        }

        public int Save(Food item)
        {
            throw new NotImplementedException();
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }


        public int TotalItemCount(string group, int status = 0)
        {
            using (IDbConnection conn = OpenConnection())
            {
                var query = new StringBuilder();
                query.Append("SELECT COUNT(*) AS TotalItems FROM dbo.FoodAbbrev ");
                if (!string.IsNullOrWhiteSpace(group))
                    query.Append("WHERE GroupCd = @groupId ");

                if (status > 0)
                {
                    if (!string.IsNullOrWhiteSpace(group))
                        query.Append(" AND ");
                    else
                        query.Append(" WHERE ");

                    query.Append(" Status = @Status ");
                }


                var parameters = new
                {
                    groupId = group,
                    Status = status
                };

                return conn.Query<int>(query.ToString(), parameters).Single();
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "SELECT [Id],[Name],[ReOrder] " +
                                     "FROM [Smoothie].[dbo].[Category] ";
                return conn.Query<Category>(query);
            }
        }


        public void Update(Food food)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "UPDATE dbo.[FoodAbbrev]" +
                                    " SET GroupId = @GroupId, Status = @Status, Name = @Name, Image = @Image, GmWt_3 = @GmWt_3, GmWt_Desc3 = @GmWt_Desc3 " +
                                    " WHERE NDB_No = @NDB_No";

                var parameters = new
                {
                    GroupId = food.GroupId,
                    Status = food.Status,
                    Name = food.Name,
                    Image = food.Image,
                    GmWt_3 = food.GmWt_3,
                    GmWt_Desc3 = food.GmWt_Desc3,
                    NDB_No = food.NDB_No
                };
                conn.Execute(query, parameters);
            }
        }
    }
}

