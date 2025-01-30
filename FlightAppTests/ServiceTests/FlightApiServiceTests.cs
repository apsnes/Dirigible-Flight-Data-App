using FlightApp.Controllers;
using FlightApp.Repository;
using FlightApp.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlightAppTests.ServiceTests
{
    public class FlightApiServiceTests
    {
        private Mock<IFlightApiRepository> _mockRepository;
        private FlightApiService _service;
        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IFlightApiRepository>();
            _service = new FlightApiService(_mockRepository.Object);
        }

        [Test]
        public void GetFlightByIata_Invokes_Once()
        {
            //Arrange
            //Act
            _service.GetFlightByIata("test");

            //Assert
            _mockRepository.Verify(x => x.GetFlightByIata(It.IsAny<string>()), Times.Once);
        }
    }
}
