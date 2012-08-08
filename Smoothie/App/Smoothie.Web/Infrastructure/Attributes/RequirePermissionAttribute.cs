using System;
using System.Web;
using System.Web.Mvc;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Services;
using Smoothie.Web.Infrastructure.Binders;
using AutoMapper;

namespace Smoothie.Web.Infrastructure.Filters
{
    public class RequirePermissionAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        #region Fields

        private readonly RoleType _roles;
        
        #endregion

        #region Properties

        public IAuthorizationService AuthorizationService { get; set; }

        #endregion

        #region Constructors

        public RequirePermissionAttribute(RoleType roles)
        {
            _roles = roles;
        }

        #endregion

        #region Methods

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // Get the current user, you could store in session or the HttpContext if you to.
            // It would be set inside teh FormsAuthenticationService
            User userSession = null;

            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var userData = (UserDataDto)UserData.User<UserDataDto>(filterContext.HttpContext);

                if (userData != null)
                    userSession = Mapper.Map<UserDataDto, User>(userData);

            }

            var success = AuthorizationService.Authorize(userSession, _roles);

            if (success)
            {
                // Since authorization is performed at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether or not a page should be served from the cache.
                var cache = filterContext.HttpContext.Response.Cache;

                cache.SetProxyMaxAge(new TimeSpan(0));
                cache.AddValidationCallback((HttpContext context, object data, ref HttpValidationStatus validationStatus) =>
                {
                    validationStatus = this.OnCacheAuthorization(new HttpContextWrapper(context));
                }, null);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }




        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Ajax requests will return status code 500 because we don't want to return the result of the
            // redirect to the login page.
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(500);
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

            filterContext.HttpContext.Response.Redirect("~/admin/login");
        }


        public HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            var userSession = (User)httpContext.Session["CurrentUser"];

            var success = AuthorizationService.Authorize(userSession, _roles);

            if (success)
            {
                return HttpValidationStatus.Valid;
            }
            else
            {
                return HttpValidationStatus.IgnoreThisRequest;
            }
        }

        #endregion

    }
}