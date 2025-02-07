using FlightApp.Controllers;
using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using NUnit;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NUnit.Framework.Legacy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FlightAppLibrary.Models.Response;


namespace FlightAppTests.ControllerTests
{

    public class AccountControllerTests
    {
        private Mock<IAccountService> _accountServiceMock;
        private AccountController _accountController;
        private DefaultHttpContext _httpContext;
        [SetUp]
        public void Setup()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _accountController = new AccountController(_accountServiceMock.Object);
            _httpContext = new DefaultHttpContext();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "validUserId"),
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim("Id", "1")
            }, "mock"));
            _httpContext.User = user;
            _accountController.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContext
            };
        }

        [Test]
        public async Task ValidSignIn()
        {
            var username = "valid@example.com";
            var password = "Password123!";


            _accountServiceMock.Setup(x => x.SignIn(It.IsAny<SignInRequestDTO>()))
                               .ReturnsAsync(new SignInResponseDTO { IsAuthSuccessful = true });

            SignInRequestDTO signInRequestDTO = new()
            {
                UserName = username,
                Password = password
            };
            var result = await _accountController.SignIn(signInRequestDTO);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            ClassicAssert.NotNull(okResult);
            ClassicAssert.IsTrue(((SignInResponseDTO)okResult.Value).IsAuthSuccessful);
            _accountServiceMock.Verify(x => x.SignIn(It.IsAny<SignInRequestDTO>()), Times.Once());

        }
        [Test]
        public async Task UnauthorizedSignIn()
        {
            var username = "unauthorized@example.com";
            var password = "Password123!";


            _accountServiceMock.Setup(x => x.SignIn(It.IsAny<SignInRequestDTO>()))
                               .ReturnsAsync(new SignInResponseDTO { IsAuthSuccessful = false });

            SignInRequestDTO signInRequestDTO = new()
            {
                UserName = username,
                Password = password
            };
            var result = await _accountController.SignIn(signInRequestDTO);

            Assert.That(result, Is.TypeOf<UnauthorizedObjectResult>());
            var unauthorizedResult = result as UnauthorizedObjectResult;
            ClassicAssert.NotNull(unauthorizedResult);
            ClassicAssert.IsFalse(((SignInResponseDTO)unauthorizedResult.Value).IsAuthSuccessful);
            _accountServiceMock.Verify(x => x.SignIn(It.IsAny<SignInRequestDTO>()), Times.Once());

        }
        [Test]
        public async Task InvalidSignIn()
        {
            var username = "invalidexample.com";
            var password = "rdew";


            _accountServiceMock.Setup(x => x.SignIn(It.IsAny<SignInRequestDTO>()))
                               .ReturnsAsync(new SignInResponseDTO { IsAuthSuccessful = false });

            SignInRequestDTO signInRequestDTO = new()
            {
                UserName = username,
                Password = password
            };
            signInRequestDTO = null;
            var result = await _accountController.SignIn(signInRequestDTO);

            Assert.That(result, Is.TypeOf<BadRequestResult>());
            _accountServiceMock.Verify(x => x.SignIn(It.IsAny<SignInRequestDTO>()), Times.Never());

        }

        [Test]
        public async Task ValidSignUp()
        {
            SignUpRequestDTO signUpRequestDTO = new()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@test.com",
                Password = "Testing123!",
                ConfirmPassword = "Testing123!",
                Karma = 9,
                DisplayName = "Test",
                Pronouns = "Mr",
                PhoneNumber = "077123123123",
            };
            _accountServiceMock.Setup(x => x.Register(It.IsAny<SignUpRequestDTO>()))
                .ReturnsAsync(new SignUpResponseDTO { IsRegistrationSuccessful = true });
            var result = await _accountController.SignUp(signUpRequestDTO);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            _accountServiceMock.Verify(x => x.Register(It.IsAny<SignUpRequestDTO>()), Times.Once());
        }
        [Test]
        public async Task WrongPasswordSignUp()
        {
            SignUpRequestDTO signUpRequestDTO = new()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@test.com",
                Password = "Testing1234!",
                ConfirmPassword = "Testing123!",
                Karma = 9,
                DisplayName = "Test",
                Pronouns = "Mr",
                PhoneNumber = "077123123123",
            };
            _accountServiceMock.Setup(x => x.Register(It.IsAny<SignUpRequestDTO>()))
                .ReturnsAsync(new SignUpResponseDTO { IsRegistrationSuccessful = false });
            var result = await _accountController.SignUp(signUpRequestDTO);
            Assert.That(result, Is.TypeOf<UnauthorizedObjectResult>());
            _accountServiceMock.Verify(x => x.Register(It.IsAny<SignUpRequestDTO>()), Times.Once());
        }


        [Test]
        public async Task InValidSignUp()
        {
            SignUpRequestDTO signUpRequestDTO = new()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Testtest.com",
                Password = "Testing1234!",
                ConfirmPassword = "Testing123!",
                Karma = 9,
                DisplayName = "Test",
                Pronouns = "Mr",
                PhoneNumber = "077123123123",
            };
            signUpRequestDTO = null;
            _accountServiceMock.Setup(x => x.Register(It.IsAny<SignUpRequestDTO>()))
                .ReturnsAsync(new SignUpResponseDTO { IsRegistrationSuccessful = false });
            var result = await _accountController.SignUp(signUpRequestDTO);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
            _accountServiceMock.Verify(x => x.Register(It.IsAny<SignUpRequestDTO>()), Times.Never());
        }
        [Test]
        public async Task ValidGetUserDetails()
        {
            // Arrange
          
            var userDto = new UserDTO
            {
                Id = "1",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber= "077123123123",
                

            };

            _accountServiceMock.Setup(x => x.GetUserDetails(userDto.Id))
                .ReturnsAsync(userDto);

            // Act
            var result = await _accountController.GetUserDetails();

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            ClassicAssert.AreEqual(userDto, okResult.Value);
        }
        [Test]
        public async Task UserNotExistGetUserDetails()
        {
            // Arrange

            var userDto = new UserDTO
            {
                Id = "21",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber = "077123123123",


            };

            _accountServiceMock.Setup(x => x.GetUserDetails(userDto.Id))
                .ReturnsAsync(userDto);

            // Act
            var result = await _accountController.GetUserDetails();

            // Assert
            ClassicAssert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = result as NotFoundResult;
            ClassicAssert.IsNotNull(notFoundResult);
        }
        [Test]
        public async Task ValidGetUserDetailsByEmail()
        {
            // Arrange

            var userDto = new UserDTO
            {
                Id = "1",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber = "077123123123",


            };

            _accountServiceMock.Setup(x => x.GetUserDetailsByEmail(userDto.Email))
                .ReturnsAsync(userDto);

            // Act
            var result = await _accountController.GetUserDetailsByEmail(userDto.Email);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            ClassicAssert.AreEqual(userDto, okResult.Value);
        }
        [Test]
        public async Task UserNotExistGetUserDetailsByEmail()
        {
            // Arrange

            var userDto = new UserDTO
            {
                Id = "21",
                Email = "testing@test.com",
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber = "077123123123",


            };

            _accountServiceMock.Setup(x => x.GetUserDetailsByEmail(userDto.Email))
                .ReturnsAsync((UserDTO)null);

            // Act
            var result = await _accountController.GetUserDetailsByEmail(userDto.Email);

            // Assert
            ClassicAssert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = result as NotFoundResult;
            ClassicAssert.IsNotNull(notFoundResult);
        }
        [Test]
        public async Task ValidUpdateUser()
        {
            UserDTO userDto = new()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@test.com",
                Karma = 9,
                DisplayName = "Test",
                Pronouns = "Mr",
                PhoneNumber = "077123123123",
            };
            _accountServiceMock.Setup(x => x.UpdateUser("1",It.IsAny<UserDTO>()))
                .ReturnsAsync(new ResponseItem { IsSuccess=true});
            var result = await _accountController.UpdateUser(userDto);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            _accountServiceMock.Verify(x => x.UpdateUser("1", It.IsAny<UserDTO>()), Times.Once());
        }
        [Test]
        public async Task UpdateUserNotExist()
        {
            UserDTO userDto = new()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                Karma = 9,
                DisplayName = "Test",
                Pronouns = "Mr",
                PhoneNumber = "077123123123",
            };
            _accountServiceMock.Setup(x => x.UpdateUser("19", It.IsAny<UserDTO>()))
                .ReturnsAsync(new ResponseItem { IsSuccess = false });
            var result = await _accountController.UpdateUser(userDto);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            _accountServiceMock.Verify(x => x.UpdateUser("1", It.IsAny<UserDTO>()), Times.Once());
        }
        [Test]
        public async Task ValidUpdateUserByAdmin()
        {
            UserDTO userDto = new()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@test.com",
                Karma = 9,
                DisplayName = "Test",
                Pronouns = "Mr",
                PhoneNumber = "077123123123",
            };
            _accountServiceMock.Setup(x => x.UpdateUser("1", It.IsAny<UserDTO>()))
                .ReturnsAsync(new ResponseItem { IsSuccess = true });
            var result = await _accountController.UpdateUserByAdmin(userDto,"1");
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            _accountServiceMock.Verify(x => x.UpdateUser("1", It.IsAny<UserDTO>()), Times.Once());
        }
        [Test]
        public async Task UpdateUserbyAdminNotExist()
        {
            UserDTO userDto = new()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                Karma = 9,
                DisplayName = "Test",
                Pronouns = "Mr",
                PhoneNumber = "077123123123",
            };
            _accountServiceMock.Setup(x => x.UpdateUser("19", It.IsAny<UserDTO>()))
                .ReturnsAsync(new ResponseItem { IsSuccess = false });
            var result = await _accountController.UpdateUserByAdmin(userDto,"19");
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            _accountServiceMock.Verify(x => x.UpdateUser("19", It.IsAny<UserDTO>()), Times.Once());
        }

      
        [Test]
        public async Task ValidResetPassword()
        {
            PasswordResetDto passwordResetDto = new PasswordResetDto()
            {
                Email = "Test@test.com",
                Password="Password123!",
                ConfirmPassword="Password123!"
            };
            
            _accountServiceMock.Setup(x => x.ResetPassword(passwordResetDto))
                .ReturnsAsync(new ResponseItem { IsSuccess = true });
            var result = await _accountController.ResetPassword(passwordResetDto);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            _accountServiceMock.Verify(x => x.ResetPassword(passwordResetDto), Times.Once());
        }
        [Test]
        public async Task InValidResetPassword()
        {
            PasswordResetDto passwordResetDto = new PasswordResetDto()
            {
                Email = "Testtest.com",
                Password = "Password123!",
                ConfirmPassword = "Password321!"
            };

            _accountServiceMock.Setup(x => x.ResetPassword(passwordResetDto))
                .ReturnsAsync(new ResponseItem { IsSuccess = false });
            var result = await _accountController.ResetPassword(passwordResetDto);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            _accountServiceMock.Verify(x => x.ResetPassword(passwordResetDto), Times.Never());
        }
        [Test]
        public async Task ValidUpdatePassword()
        {
            PasswordUpdateDto passwordUpdateDto = new PasswordUpdateDto()
            {
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            _accountServiceMock.Setup(x => x.UpdatePassword("1",passwordUpdateDto))
                .ReturnsAsync(new ResponseItem { IsSuccess = true });
            var result = await _accountController.UpdatePassword(passwordUpdateDto);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            _accountServiceMock.Verify(x => x.UpdatePassword("1",passwordUpdateDto), Times.Once());
        }
       
        [Test]
        public async Task InValidUpdatePassword()
        {
            PasswordUpdateDto passwordUpdateDto = new PasswordUpdateDto()
            {
                Password = "Password123!",
                ConfirmPassword = "Passord123!"
            };

            _accountServiceMock.Setup(x => x.UpdatePassword("1", passwordUpdateDto))
                .ReturnsAsync(new ResponseItem { IsSuccess = false });
            var result = await _accountController.UpdatePassword(passwordUpdateDto);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            _accountServiceMock.Verify(x => x.UpdatePassword("1", passwordUpdateDto), Times.Never());
        }
        [Test]
        public async Task ValidUpdateRoles()
        {
            string userId = "1";
            string role = "Admin";
            RolesUpdateDto roleDto = new RolesUpdateDto()
            {
                UserId = userId,
                Role = role
            };
            

            _accountServiceMock.Setup(x => x.UpdateUserRoles(role, userId))
                .ReturnsAsync(new ResponseItem() { IsSuccess = true, Message="" });
            var result = await _accountController.UpdateUserRoles(roleDto);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            _accountServiceMock.Verify(x => x.UpdateUserRoles(role, userId), Times.Once());
        }

        [Test]
        public async Task InValidUpdateRoles()
        {
            string userId = "1";
            string role = "Administrator";
            RolesUpdateDto roleDto = new RolesUpdateDto()
            {
                UserId = userId,
                Role = role
            };


            _accountServiceMock.Setup(x => x.UpdateUserRoles(role,userId))
                .ReturnsAsync(new ResponseItem() { IsSuccess = false, Message = "" });
            var result = await _accountController.UpdateUserRoles(roleDto);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            _accountServiceMock.Verify(x => x.UpdateUserRoles(role, userId), Times.Once());
        }
        [Test]
        public async Task ValidAssignRoles()
        {
           
            string role = "Admin";
            RoleAssignmentItem roleDto = new ()
            {
                Email="test@test.com",
                Role = role
            };


            _accountServiceMock.Setup(x => x.AssignRoleToUser(roleDto.Email, role))
                .ReturnsAsync(new ResponseItem() { IsSuccess = true, Message = "" });
            var result = await _accountController.AssignRole(roleDto);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            _accountServiceMock.Verify(x => x.AssignRoleToUser(roleDto.Email, role), Times.Once());
        }

        [Test]
        public async Task InValidAssignRoles()
        {
            string role = "Admin";
            RoleAssignmentItem roleDto = new()
            {
                Email = "testing@test.com",
                Role = role
            };


            _accountServiceMock.Setup(x => x.AssignRoleToUser(roleDto.Email, role))
                .ReturnsAsync(new ResponseItem() { IsSuccess = false, Message = "" });
            var result = await _accountController.AssignRole(roleDto);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            _accountServiceMock.Verify(x => x.AssignRoleToUser(roleDto.Email, role), Times.Once());
        }
    }
   
}
