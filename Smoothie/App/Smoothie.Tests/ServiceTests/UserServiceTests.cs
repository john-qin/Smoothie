using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Smoothie.Domain;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.Repositories;
using Smoothie.Services;
using Smoothie.Web.Infrastructure;

namespace Smoothie.Tests.ServiceTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;
        private List<User> _users;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _users = new List<User>
                         {
                            new User { Id = 1, Email = "test@hotmail.com", Password = "" },
                            new User { Id = 1, Email = "test2@hotmail.com", Password = "123456".Hash() },
                            new User { Id = 2, Email = "9422722@twitter.com", Password = "" },
                            new User { Id = 3, Email = "john.test@test.com", Password = "12345".Hash() }
                         };
        }

        [Test]
        public void AddUser_adding_a_nonexist_user_should_return_success_confirmation()
        {
            // Arrange
            int userId = 5;
            var user = new User { Email = "newuser@test.com", Password = "1234567".Hash() };
            _mockUserRepository.Setup(r => r.GetUserByEmail(user.Email, AccountType.Smoothie)).Returns((User) null);
            _mockUserRepository.Setup(r => r.Save(user)).Returns(userId);

            _userService = new UserService(_mockUserRepository.Object);

            // Act
            ActionConfirmation<User> confirmation = _userService.AddUser(user, AccountType.Smoothie);

            // Assert
            Assert.True(confirmation.WasSuccessful);
            Assert.That(confirmation.Message, Is.EqualTo(""));
            Assert.That(confirmation.Value, Is.EqualTo(user));
            Assert.That(confirmation.Value.Id, Is.EqualTo(userId));

        }

        [Test]
        public void AddUser_adding_an_exist_user_should_return_unsuccess_confirmation()
        {
            // Arrange
            int userId = 5;
            var user = new User { Email = "test@hotmail.com", Password = ""};
            _mockUserRepository.Setup(r => r.GetUserByEmail(user.Email, AccountType.Smoothie)).Returns(user);
            _mockUserRepository.Setup(r => r.Save(user)).Returns(userId);

            _userService = new UserService(_mockUserRepository.Object);

            // Act
            ActionConfirmation<User> confirmation = _userService.AddUser(user, AccountType.Smoothie);

            // Assert
            Assert.False(confirmation.WasSuccessful);
            Assert.IsNull(confirmation.Value);
            Assert.AreEqual(confirmation.Message, "This Email already exists");
        }

    }
}
