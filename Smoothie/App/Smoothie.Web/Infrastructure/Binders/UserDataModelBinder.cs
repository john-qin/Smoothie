using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ServiceStack.Text;

namespace Smoothie.Web.Infrastructure.Binders
{
    public class UserDataModelBinder<T> : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return UserData.User<T>(controllerContext.RequestContext.HttpContext);
        }


    }


    public static class UserData
    {
        public static object User<T>(System.Web.HttpContextBase context) //(ControllerContext controllerContext)
        {
            //if (controllerContext.RequestContext.HttpContext.Request.IsAuthenticated)
            if (context.Request.IsAuthenticated)
            {
                var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
                   // controllerContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

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