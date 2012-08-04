using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels;

namespace Smoothie.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByLogin(UserLoginViewModel userLogin, AccountType accountType);
        User GetUserByEmail(string email, AccountType accountType);
        void UpdatePassword(string password, string email, AccountType accountType);
    }
}
