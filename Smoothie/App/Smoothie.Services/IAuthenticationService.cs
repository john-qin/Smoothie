
using System.Web;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels;

namespace Smoothie.Services
{
    public interface IAuthenticationService
    {

        int SetAuthCookie<T>(HttpResponseBase responseBase, string name, bool rememberMe, T userData);
        ActionConfirmation<User> Login(UserLoginViewModel userLogin, AccountType accountType);
        void Logout();
    }
}
