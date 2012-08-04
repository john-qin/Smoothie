using System;
using System.Web.Mvc;
using System.Web.Security;
using AttributeRouting.Web.Mvc;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels;
using Smoothie.Services;
using Smoothie.Web.Infrastructure;
using Twitterizer;

namespace Smoothie.Web.Controllers
{
    public partial class AccountController : Controller
    {
        private  string _twitterConsumerKey;
        private  string _twitterConsumerSecret;
        private  string _twitterAccessToken;
        private  string _twitterAccessTokenSecret;

        private readonly IUserService _userService;
        private readonly IMappingService _mappingService;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IUserService userService, IMappingService mappingService, IAuthenticationService authenticationService)
        {
            if (userService == null)
                throw new ArgumentNullException("userService");

            if (mappingService == null)
                throw new ArgumentNullException("mappingService");

            if (authenticationService == null)
                throw new ArgumentNullException("authenticationService");

            _userService = userService;
            _mappingService = mappingService;
            _authenticationService = authenticationService;
        }

        [GET("login")]
        public virtual ActionResult Login()
        {

            return View(MVC.Account.Views.Login, new UserLoginViewModel());
        }

        [POST("login")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(UserLoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var confirmation = _authenticationService.Login(user, AccountType.Smoothie);

                if (confirmation.WasSuccessful)
                {
                    var userData = _mappingService.Map<User, UserDataDto>(confirmation.Value);
                    _authenticationService.SetAuthCookie(Response, userData.DisplayName, false, userData);
                    return RedirectToAction(MVC.Home.Index());
                }

                ModelState.AddModelError("", confirmation.Message);
            }
            return View(MVC.Account.Views.Login, user);
        }



        public virtual ActionResult FacebookLogin(string code, string state, string returnUrl)
        {
            string redirectUrl = String.Format("{0}{1}{2}{3}", Request.Url.Scheme, Uri.SchemeDelimiter, Request.Url.Authority, Request.Url.AbsolutePath);

            if (string.IsNullOrWhiteSpace(code) && string.IsNullOrWhiteSpace(state))
            {
                return Redirect(Infrastructure.Facebook.LogOn(redirectUrl, returnUrl));
            }
            else
            {
                var newUser = Infrastructure.Facebook.OAuth(code, state, redirectUrl, Request.Url.ToString(), _mappingService);

                if (newUser != null)
                {
                    var confirmation = _userService.AddUser(newUser, AccountType.Facebook);
                }

                // prevent open redirection attack by checking if the url is local.
                if (Url.IsLocalUrl(state))
                    return Redirect(state);
                else
                    return RedirectToAction("Index", "Home");
            }
        }



        public virtual ActionResult TwitterLogin(string oauth_token, string oauth_verifier, string returnUrl, string denied)
        {
            _twitterConsumerKey = TwitterSettings.Settings.ConsumerKey;
            _twitterConsumerSecret = TwitterSettings.Settings.ConsumerSecret;
            _twitterAccessToken = TwitterSettings.Settings.AccessToken;
            _twitterAccessTokenSecret = TwitterSettings.Settings.AccessTokenSecret;

            if (!string.IsNullOrWhiteSpace(denied))
                return Redirect("/");


            if (string.IsNullOrWhiteSpace(oauth_token) || string.IsNullOrWhiteSpace(oauth_verifier))
            {
                var token = Twitter.LogOn(Request.Url.ToString(), returnUrl, _twitterConsumerKey,
                                                         _twitterConsumerSecret);

                return Redirect(OAuthUtility.BuildAuthorizationUri(token, true).ToString());

            }

           var newUser = Twitter.GetUser(_twitterConsumerKey, _twitterConsumerSecret, oauth_token, oauth_verifier);

            if (newUser != null)
            {
                var confirmation = _userService.AddUser(newUser, AccountType.Twitter);
            }

            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);

        }


        [GET("signup")]
        public virtual ActionResult Signup()
        {
            return View(MVC.Account.Views.Signup, new UserRegisterViewModel());
        }

        [POST("signup")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Signup(UserRegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var newUser = _mappingService.Map<UserRegisterViewModel, User>(user);

                var confirmation = _userService.AddUser(newUser, AccountType.Smoothie);

                if (confirmation.WasSuccessful)
                    return RedirectToAction(MVC.Account.Login());

                ModelState.AddModelError("Email", confirmation.Message);
            }
            return View(user);
        }



        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            /*
            var userId = UserInfo.GetId();
            if (userId == 0)
                throw new ArgumentNullException("UserId");

            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == userId);

            if (user != null)
            {

                if (user.AccountType == (int)AccountType.Facebook)
                {
                    if (!string.IsNullOrWhiteSpace((string)Session["fbToken"]))
                    {
                        var logoutLink = string.Format("https://www.facebook.com/logout.php?next={0}&access_token={1}",
                                                       Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, ""),
                                                       Session["fbToken"]);

                        ClearSession();
                        return Redirect(logoutLink);
                    }
                }
                else if (user.AccountType == (int)AccountType.Twitter)
                {

                    OAuthTokens tokens = new OAuthTokens()
                    {
                        AccessToken = _twitterAccessToken,
                        AccessTokenSecret = _twitterAccessTokenSecret,
                        ConsumerKey = _twitterConsumerKey,
                        ConsumerSecret = _twitterConsumerSecret
                    };

                    TwitterResponse<TwitterErrorDetails> twitterResponse = TwitterAccount.EndSession(tokens, null);
                }
            }

            ClearSession();
             */
            return RedirectToAction("Index", "Home");
        }


        /*
         * 
         * 
         * 
        private void ClearSession()
        {
            Session.Abandon();
            var authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };

            Response.Cookies.Add(authenticationCookie);

            var aspNetCookie = new HttpCookie("ASP.NET_SessionId", "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(aspNetCookie);

        }
         * 
         */

    }
}
