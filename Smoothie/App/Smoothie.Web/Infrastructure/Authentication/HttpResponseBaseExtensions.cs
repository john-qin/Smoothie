using System;
using System.Web;
using System.Web.Security;
using ServiceStack.Text;

namespace Smoothie.Web.Infrastructure.Authentication
{
    public static class HttpResponseBaseExtensions
    {
        public static int SetAuthCookie<T>(this HttpResponseBase responseBase, string name, bool rememberMe, T userData)
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
    }
}