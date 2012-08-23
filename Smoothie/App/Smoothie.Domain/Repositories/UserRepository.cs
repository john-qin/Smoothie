using System.Data;
using System.Linq;
using Dapper;
using Smoothie.Domain.Entities;

namespace Smoothie.Domain.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public User GetUserByLogin(ViewModels.UserLoginViewModel userLogin, Enums.AccountType accountType)
        {

            using (IDbConnection conn = OpenConnection())
            {
                const string query = "SELECT u.Id, u.Email,u.Password, u.Firstname,u.Lastname,u.CreatedDate,u.LastLogin," +
                                    "       u.AccountType, u.Roles, u.Displayname, u.Avatar, u.ThirdPartyId, u.Status, u.Ip, u.IsAdmin" +
                                    " FROM dbo.[User] AS u" +
                                    " WHERE u.Email = @email AND u.Password = @password AND u.AccountType = accountType";

                var parameters = new
                                     {
                                         email = userLogin.Email,
                                         password = userLogin.Password
                                     };

                return conn.Query<User>(query, parameters).SingleOrDefault();



            }

        }

        public User GetUserByEmail(string email, Enums.AccountType accountType)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "SELECT u.Id, u.Email,u.Password, u.Firstname,u.Lastname,u.CreatedDate,u.LastLogin," +
                                    "       u.AccountType, u.Roles, u.Displayname, u.Avatar, u.ThirdPartyId, u.Status, u.Ip, u.IsAdmin" +
                                    " FROM dbo.[User] AS u" +
                                    " WHERE u.Email = @Email AND u.AccountType = @AccountType";

                return conn.Query<User>(query, new {Email = email, AccountType = accountType}).FirstOrDefault();
            }
        }

        public void UpdatePassword(string password, string email, Enums.AccountType accountType)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "UPDATE dbo.[User]" +
                                    " SET Password = @Password" +
                                    " WHERE AccountType = @AccountType AND Email = @Email AND Password = @Password";

                var parameters = new
                                     {
                                         AccountType = accountType,
                                         Email = email,
                                         Password = password
                                     };
                conn.Execute(query, parameters);
            }
        }

        public User Get(int id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "SELECT u.Id, u.Email,u.Password, u.Firstname,u.Lastname,u.CreatedDate,u.LastLogin," +
                                    "       u.AccountType, u.Roles, u.Displayname, u.Avatar, u.ThirdPartyId, u.Status, u.Ip, u.IsAdmin" +
                                    " FROM dbo.[User] AS u" +
                                    " WHERE u.Id = @Id";

                return conn.Query<User>(query, new { Id = id }).SingleOrDefault();
            }
        }

        public int Save(User item)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "INSERT INTO dbo.[User]" +
                                     "        ( Email ,Password ,Firstname ,Lastname ,CreatedDate ,LastLogin , AccountType ," +
                                     "          Roles ,Displayname ,Avatar ,ThirdPartyId ,Status ,Ip, IsAdmin" +
                                     "        )" +
                                     "VALUES  ( @Email, @Password , @Firstname , @Lastname , @CreatedDate , @LastLogin ," +
                                     "          @AccountType , @Roles , @Displayname , @Avatar , @ThirdPartyId," +
                                     "          @Status , @Ip, @IsAdmin" +
                                     "        )";

                var parameters = new
                                     {
                                         Email = item.Email,
                                         Password = item.Password,
                                         Firstname = item.Firstname,
                                         Lastname = item.Lastname,
                                         CreatedDate = item.CreatedDate,
                                         LastLogin = item.LastLogin,
                                         AccountType = item.AccountType,
                                         Roles = item.Roles,
                                         Displayname = item.DisplayName,
                                         Avatar = item.Avatar,
                                         ThirdPartyId = item.ThirdPartyId,
                                         Status = item.Status,
                                         Ip = item.Ip,
                                         IsAdmin = item.IsAdmin
                                     };

                conn.Execute(query, parameters);
                SetIdentity<int>(conn, id=> item.Id = id);
                return item.Id;
            }
        }

        public int Delete(int id)
        {
            using(IDbConnection conn = OpenConnection())
            {
                const string query = "DELETE FROM dbo.[User] WHERE Id = @Id";
                int rowsAffected = conn.Execute(query, new {Id = id});
                return rowsAffected;
            }
        }

 
    }
}
