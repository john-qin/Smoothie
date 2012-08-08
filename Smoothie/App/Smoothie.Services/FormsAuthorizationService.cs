using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;

namespace Smoothie.Services
{
    public class FormsAuthorizationService : IAuthorizationService
    {

        public bool Authorize(User user, RoleType requiredRoles)
        {
            if (user == null)
                return false;
            if (user.IsAdmin)
                return true;

            // Check if the roles enum has the specific role bit set.
            return (requiredRoles & user.Roles) == requiredRoles;
        }
    }
}
