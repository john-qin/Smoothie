using System;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels;
using Smoothie.Services;
using Smoothie.Web.Infrastructure.Filters;

namespace Smoothie.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public partial class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMappingService _mappingService;
        private readonly IAuthenticationService _authenticationService;

        public HomeController(IUserService userService, IMappingService mappingService, IAuthenticationService authenticationService)
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


        [RequirePermission(RoleType.Administrator)]
        [GET("Index")]
        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.Admin.Food.Index());
        }

        
        [GET("Login")]
        public virtual ActionResult Login()
        {
            return View(new UserLoginViewModel());
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
                    return RedirectToAction(MVC.Admin.Home.Index());
                }

                ModelState.AddModelError("", confirmation.Message);
            }
            return View(MVC.Account.Views.Login, user);
        }

    }
}
