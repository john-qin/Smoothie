using System.Web;
using System.Web.Security;
using ServiceStack.Text;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.Repositories;

namespace Smoothie.Services
{
    public class FormsAuthenticationService : IAuthenticationService
    {

        private readonly IUserRepository _userRepository;

        public FormsAuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public int SetAuthCookie<T>(HttpResponseBase responseBase, string name, bool rememberMe, T userData)
        {
            // in order to pickup teh settings from config,
            // we create a default cookie and use its values to create a new one.

            var cookie = FormsAuthentication.GetAuthCookie(name, rememberMe);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                                                          ticket.Expiration, ticket.IsPersistent, userData.ToJson(),
                                                          ticket.CookiePath);
            var encTicket = FormsAuthentication.Encrypt(newTicket);

            cookie.Value = encTicket;
            responseBase.Cookies.Add(cookie);

            return encTicket.Length;
        }



        public ActionConfirmation<User> Login(Domain.ViewModels.UserLoginViewModel userLogin, AccountType accountType)
        {
            ActionConfirmation<User> confirmation;

            var user = _userRepository.GetUserByLogin(userLogin, AccountType.Smoothie);

            if (user != null)
            {
                confirmation = new ActionConfirmation<User>
                {
                    WasSuccessful = true,
                    Message = "",
                    Value = user
                };
            }
            else
            {
                confirmation = new ActionConfirmation<User>
                {
                    WasSuccessful = false,
                    Message = "Invalid user Email or password",
                    Value = null
                };
            }

            return confirmation;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

   
   
    }
}
