
using System.Web;

namespace Smoothie.Services
{
    public class FormsAuthorizationService : IAuthorizationService
    {
        //private readonly HttpContextBase _httpContext;

        //public FormsAuthorizationService(HttpContextBase httpContext)
        //{
        //    _httpContext = httpContext;
        //}

        public bool Authorize(Domain.Entities.User user, Domain.Enums.RoleType requiredRoles)
        {
            if (user == null)
                return false;
            if (user.IsAdministrator)
                return true;

            // Check if the roles enum has the specific role bit set.
            return (requiredRoles & user.Roles) == requiredRoles;
        }
    }
}
