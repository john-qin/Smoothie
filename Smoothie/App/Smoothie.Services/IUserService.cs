using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels;

namespace Smoothie.Services
{
    public interface IUserService
    {
        ActionConfirmation<User> AddUser(User user, AccountType accountType);
    }
}
