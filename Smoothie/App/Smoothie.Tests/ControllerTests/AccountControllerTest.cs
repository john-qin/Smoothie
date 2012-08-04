using System.Web.Mvc;
using System.Web.Security;
using Moq;
using NUnit.Framework;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels;
using Smoothie.Services;
using Smoothie.Web.Controllers;
using Smoothie.Web.Infrastructure;

namespace Smoothie.Tests.ControllerTests
{
    [TestFixture]
    public class AccountControllerTest
    {
        private AccountController _accountController;
        private Mock<IUserService> _userService;
        private Mock<IMappingService> _mappingService;
        private Mock<IAuthenticationService> _authenticationService;

        [SetUp]
        public void SetUp()
        {

            _userService = new Mock<IUserService>();
            _mappingService = new Mock<IMappingService>();
            _authenticationService = new Mock<IAuthenticationService>();
        }


        [Test]
        public void Signup_Action_Get_Returns_Signup_View()
        {
            // Arrange
            const string expectedViewName = "~/Views/Account/Signup.cshtml";
            var controller = new AccountController(_userService.Object, _mappingService.Object, _authenticationService.Object);

            // Act
            var result = controller.Signup() as ViewResult;

            // Assert
            Assert.IsNotNull(result, "should have returned a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);
        }


        [Test]
        public void Signup_Action_When_The_User_Model_Is_Valid_Returns_RedirectToRouteResult()
        {
            // Arrange
            const string expectedActionName = "Login";
            const string expectedControllerName = "Account";

            var registeredUser = new UserRegisterViewModel { Email = "newuser@test.com", Password = "123456789".Hash() };


            var confirmation = new ActionConfirmation<User>
                                   {
                                       WasSuccessful = true,
                                       Message = "",
                                       Value = new User()
                                   };
            _userService.Setup(r => r.AddUser(It.IsAny<User>(), It.IsAny<AccountType>())).Returns(confirmation);

            _accountController = new AccountController(_userService.Object, _mappingService.Object, _authenticationService.Object);

            // Act
            var result = _accountController.Signup(registeredUser) as RedirectToRouteResult;

            // Assert

            Assert.IsNotNull(result, "Should have returned a RedirectToRouteResult");
            Assert.AreEqual(expectedActionName, result.RouteValues["Action"], "Action name should be {0}", expectedActionName);
            Assert.AreEqual(expectedControllerName, result.RouteValues["Controller"], "Controller name should be {0}", expectedControllerName);
        }

        [Ignore]
        [Test]
        public void Signup_Action_When_The_User_Model_Is_InValid_Returns_Back_To_Signup_View()
        {

        }


        [Test]
        public void Login_Action_Get_Returns_Login_View()
        {
            // Arrange

            var expectedViewName = "~/Views/Account/Login.cshtml";

            _accountController = new AccountController(_userService.Object, _mappingService.Object, _authenticationService.Object);

            // Act

            var result = _accountController.Login() as ViewResult;

            // Assert

            Assert.AreEqual(expectedViewName, result.ViewName, "View name should be {0}", expectedViewName);
        }

        [Test]
        public void Login_Action_When_User_Exists_Redirect_To_Home_Page()
        {
            // Arrange

            const string expectedActionName = "Index";
            const string expectedControllerName = "Home";

            var registeredUser = new UserLoginViewModel() { Email = "newuser@test.com", Password = "123456789".Hash() };
            var loggedInUser = new User { Id = 1, Email = "newuser@test.com", Password = "123456789".Hash(), DisplayName = "Mike" };

            _authenticationService.Setup(x => x.Login(registeredUser, AccountType.Smoothie)).Returns(
                new ActionConfirmation<User>
                    {
                        WasSuccessful = true,
                        Message = "",
                        Value = loggedInUser
                    }
                );

            _accountController = new AccountController(_userService.Object, _mappingService.Object, _authenticationService.Object);


            // Act

            var result = _accountController.Login(registeredUser) as RedirectToRouteResult;

            // Assert

            Assert.NotNull(result, "RedirectToRouteResult is not null");
            Assert.AreEqual(expectedActionName, result.RouteValues["Action"], "Action name should be {0}", expectedActionName);
            Assert.AreEqual(expectedControllerName, result.RouteValues["Controller"], "Controller name should be {0}", expectedControllerName);
        }


        [Test]
        public void Login_Action_When_User_Does_Not_Exist_Returns_Back_To_View()
        {
            // Arrange

            const string expectedViewName = "~/Views/Account/Login.cshtml";

            var registeredUser = new UserLoginViewModel() { Email = "newuser@test.com", Password = "123456789".Hash() };

            _authenticationService.Setup(x => x.Login(registeredUser, AccountType.Smoothie)).Returns(
                new ActionConfirmation<User>
                    {
                        WasSuccessful = false,
                        Message = "Invalid user Email or password",
                        Value = null
                    }
                );

                 _accountController = new AccountController(_userService.Object, _mappingService.Object, _authenticationService.Object);

            // Act

            var result = _accountController.Login(registeredUser) as ViewResult;

            // Assert

            Assert.AreEqual(expectedViewName, result.ViewName, "View name should be {0}", expectedViewName);

        }



    }
}
