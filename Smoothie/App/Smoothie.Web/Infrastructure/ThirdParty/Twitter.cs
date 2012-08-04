using System;
using System.Configuration;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Twitterizer;

namespace Smoothie.Web.Infrastructure
{
    public class Twitter
    {
        public static string LogOn(string currentUrl, string returnUrl, string twitterConsumerKey, string twitterConsumerSecret)
        {
            UriBuilder builder = new UriBuilder(currentUrl);
            builder.Query = string.Concat(
                builder.Query,
                string.IsNullOrWhiteSpace(builder.Query) ? string.Empty : "&",
                "returnUrl=",
                returnUrl
                );

            string token = OAuthUtility.GetRequestToken(twitterConsumerKey, twitterConsumerSecret, builder.ToString()).Token;
            return token;
        }


        public static User GetUser(string twitterConsumerKey, string twitterConsumerSecret, string oauth_token, string oauth_verifier)
        {
            var tokens = OAuthUtility.GetAccessToken(twitterConsumerKey, twitterConsumerSecret, oauth_token, oauth_verifier);

            var user = new User
            {
                Email = string.Format("{0}@twitter.com", tokens.UserId.ToString()),
                Password = "",  // have to be at least 8 characters to pass valiation
                Firstname = "",
                Lastname = "",

                CreatedDate = DateTime.Now,
                LastLogin = DateTime.Now,

                AccountType = AccountType.Twitter,
                Roles = RoleType.Member,
                DisplayName = tokens.ScreenName,
                Avatar = string.Format("https://api.twitter.com/1/users/profile_image?screen_name={0}", tokens.ScreenName),
                ThirdPartyId = tokens.UserId.ToString(),
                Status = Status.Approved,
                Ip = ""

            };

            return user;
        }
    }

    public class TwitterSettings : ConfigurationSection
    {

        private static TwitterSettings settings = ConfigurationManager.GetSection("Twitter") as TwitterSettings;
        public static TwitterSettings Settings { get { return settings; } }

        [ConfigurationProperty("ConsumerKey", IsRequired = true)]
        public string ConsumerKey
        {
            get { return (string)this["ConsumerKey"]; }
            set { this["ConsumerKey"] = value; }
        }

        [ConfigurationProperty("ConsumerSecret", IsRequired = true)]
        public string ConsumerSecret
        {
            get { return (string)this["ConsumerSecret"]; }
            set { this["ConsumerSecret"] = value; }
        }


        [ConfigurationProperty("AccessToken", IsRequired = true)]
        public string AccessToken
        {
            get { return (string)this["AccessToken"]; }
            set { this["AccessToken"] = value; }
        }

        [ConfigurationProperty("AccessTokenSecret", IsRequired = true)]
        public string AccessTokenSecret
        {
            get { return (string)this["AccessTokenSecret"]; }
            set { this["AccessTokenSecret"] = value; }
        }


    }
}