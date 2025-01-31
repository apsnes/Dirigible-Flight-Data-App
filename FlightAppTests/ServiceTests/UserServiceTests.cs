using FlightApp.Repository;
using FlightApp.Service;
using FlightApp.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppTests.ServiceTests
{
    public class UserServiceTests
    {
        private Mock<IUsersRepository> _mockRepository;
        private UsersService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IUsersRepository>();
            _service = new UsersService(_mockRepository.Object);
        }
        [Test]
        public void GetUsers_Invokes_Once()
        {
            //Arrange
            //Act
            _service.GetUsers();

            //Assert
            _mockRepository.Verify(x => x.GetUsers(), Times.Once);
        }
        [Test]
        public void AddUser_Invokes_Once()
        {
            //Arrange
            //Act
            _service.AddUser(new User());

            //Assert
            _mockRepository.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
        }
    }
}
