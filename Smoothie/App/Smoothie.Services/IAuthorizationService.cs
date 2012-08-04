using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;

namespace Smoothie.Services
{
    public interface IAuthorizationService
    {
        bool Authorize(User user, RoleType requiredRoles);
    }
}