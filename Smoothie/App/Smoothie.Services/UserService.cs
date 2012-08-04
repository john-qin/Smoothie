using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.Repositories;

namespace Smoothie.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public ActionConfirmation<User> AddUser(User user, AccountType accountType)
        {
            User existUser = _userRepository.GetUserByEmail(user.Email, accountType);
            ActionConfirmation<User> confirmation;

            if (existUser != null)
            {
                confirmation = new ActionConfirmation<User>
                                   {
                                       WasSuccessful = false,
                                       Message = "This Email already exists",
                                       Value = null
                                   };

            }
            else
            {
                int userId = _userRepository.Save(user);
                user.Id = userId;

                confirmation = new ActionConfirmation<User>
                                   {
                                       WasSuccessful = true,
                                       Message = "",
                                       Value = user
                                   };
            }

            return confirmation;


        }



    }
}
