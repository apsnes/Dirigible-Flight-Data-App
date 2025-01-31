using FlightApp.Controllers;
using FlightApp.Entities;
using FlightApp.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppTests.ControllerTests
{
    public class UserControllerTests
    {
        private Mock<IUsersService> _mockService;
        private UsersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IUsersService>();
            _controller = new UsersController(_mockService.Object);
        }

        [Test]
        public void GetUsers_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.GetUsers()).Returns(new List<User>());

            //Act
            var result = _controller.GetUsers();

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetUsers_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            List<User>? response = null;
            _mockService.Setup(x => x.GetUsers()).Returns(response);

            //Act
            var result = _controller.GetUsers();

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetUsers_InvokesServiceOnce()
        {
            //Arrange
            //Act
            _controller.GetUsers();
            //Assert
            _mockService.Verify(x => x.GetUsers(), Times.Once);
        }

        [Test]
        public void AddUser_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.AddUser(It.IsAny<User>())).Returns(new User());

            //Act
            var result = _controller.AddUser(new User());

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void AddUser_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            User? response = null;
            _mockService.Setup(x => x.AddUser(It.IsAny<User>())).Returns(response);

            //Act
            var result = _controller.AddUser(new User());

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void AddUser_InvokesServiceOnce()
        {
            //Arrange
            //Act
            _controller.AddUser(new User());

            //Assert
            _mockService.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
        }
    }
}
