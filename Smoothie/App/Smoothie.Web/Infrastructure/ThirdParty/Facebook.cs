using System;
using System.Collections.Generic;
using Facebook;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Services;

namespace Smoothie.Web.Infrastructure
{
    public class Facebook
    {
        public static string LogOn(string redirectUrl, string returnUrl)
        {
            var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
            oAuthClient.RedirectUri = new Uri(redirectUrl);
            var loginUri = oAuthClient.GetLoginUrl(new Dictionary<string, object> { { "state", returnUrl }, { "scope", "email" } });
            return loginUri.AbsoluteUri;
        }

        public static User OAuth(string code, string state, string redirectUrl, string currentUrl, IMappingService mappingService)
        {
            FacebookOAuthResult oauthResult;

            User user = null;

            if (FacebookOAuthResult.TryParse(currentUrl, out oauthResult))
            {
                if (oauthResult.IsSuccess)
                {
                    var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
                    oAuthClient.RedirectUri = new Uri(redirectUrl);
                    dynamic tokenResult = oAuthClient.ExchangeCodeForAccessToken(code);
                    string accessToken = tokenResult.access_token;

                    //DateTime expiresOn = DateTime.MaxValue;

                    if (tokenResult.ContainsKey("expires"))
                    {
                        DateTimeConvertor.FromUnixTime(tokenResult.expires);
                    }

                    var fbClient = new FacebookClient(accessToken);
                    dynamic me = fbClient.Get("me");

                    //long facebookId = Convert.ToInt64(me.id); 


                    user = new User
                    {
                        Email = me.email,
                        Password = "",
                        Firstname = me.first_name,
                        Lastname = me.last_name,

                        CreatedDate = DateTime.Now,
                        LastLogin = DateTime.Now,

                        AccountType = AccountType.Facebook,
                        Roles = RoleType.Member,
                        DisplayName = string.IsNullOrWhiteSpace(me.username) ? me.first_name : me.username,
                        Avatar = string.IsNullOrWhiteSpace(me.id.ToString()) ? "" : string.Format("https://graph.facebook.com/{0}/picture", me.id.ToString()),
                        ThirdPartyId = me.id.ToString(),
                        Status = Status.Approved,
                        Ip = ""
                    };

                }
            }

            return user;
        }
    }
}