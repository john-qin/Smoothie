using System.Web.Mvc;
using System.Web.Security;
using ServiceStack.Text;

namespace Smoothie.Web.Infrastructure.Binders
{
    public class UserDataModelBinder<T> : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            /*
            if (controllerContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                var cookie =
                    controllerContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (cookie == null)
                    return null;

                var decrypted = FormsAuthentication.Decrypt(cookie.Value);

                if (!string.IsNullOrWhiteSpace(decrypted.UserData))
                    return JsonSerializer.DeserializeFromString<T>(decrypted.UserData);
            }

            return null;
             */
            return UserData.User<T>(controllerContext);
        }


    }


    public static class UserData
    {
        public static object User<T>(ControllerContext controllerContext)
        {
            if (controllerContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                var cookie =
                    controllerContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (cookie == null)
                    return null;

                var decrypted = FormsAuthentication.Decrypt(cookie.Value);

                if (!string.IsNullOrWhiteSpace(decrypted.UserData))
                    return JsonSerializer.DeserializeFromString<T>(decrypted.UserData);
            }

            return null;
        }
    }
}