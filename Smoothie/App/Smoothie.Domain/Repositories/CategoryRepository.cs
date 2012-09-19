using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {


        public int Save(Entities.Category item)
        {
            using (var conn = OpenConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", item.Id);
                parameter.Add("@Name", item.Name);
                parameter.Add("@ReOrder", item.ReOrder);
                parameter.Add("@Status", item.Status);

                parameter.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                conn.Execute("SaveCategory", parameter, commandType: CommandType.StoredProcedure);

                return parameter.Get<int>("@Result");
            }
        }



        public IEnumerable<Category> GetAll()
        {
            using (IDbConnection conn = OpenConnection())
            {
                var query = new StringBuilder();
                query.Append("SELECT [id], ");
                query.Append("       [name], ");
                query.Append("       [reorder], ");
                query.Append("       [status] ");
                query.Append("FROM   [Smoothie].[dbo].[category] order by status desc, reorder");

                return conn.Query<Category>(query.ToString());
            }
        }

        public Category Get(int id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                var query = new StringBuilder();
                query.Append("SELECT [id], ");
                query.Append("       [name], ");
                query.Append("       [reorder], ");
                query.Append("       [status] ");
                query.Append("FROM   [Smoothie].[dbo].[category] Where Id = @Id");

                var parameter = new { Id = id };

                return conn.Query<Category>(query.ToString(), parameter).FirstOrDefault();
            }
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
