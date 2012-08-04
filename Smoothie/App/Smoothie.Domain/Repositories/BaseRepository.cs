using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Smoothie.Domain.Repositories
{
    public abstract class BaseRepository
    {
        protected static void SetIdentity<T>(IDbConnection connection, Action<T> setId)
        {
            dynamic identity = connection.Query("SELECT @@IDENTITY AS Id").Single();
            var newId = (T) identity.Id;
            setId(newId);
        }

        protected static IDbConnection OpenConnection()
        {
            IDbConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SmoothieConn"].ConnectionString);
            connection.Open();
            return connection;
        }

    }
}
