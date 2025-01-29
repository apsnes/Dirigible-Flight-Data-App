using FlightApp.Controllers;
using FlightApp.Models.Response;
using FlightApp.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlightAppTests.ControllerTests
{
    public class FlightApiServiceTests
    {
        private Mock<IFlightApiService> _mockService;
        private FlightApiController _controller;
        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IFlightApiService>();
            _controller = new FlightApiController(_mockService.Object);
        }

        [Test]
        public void GetFlightByIata_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(new FlightResponseWrapper());
            
            //Act
            var result = _controller.GetFlightByIata("test");

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
        [Test]
        public void GetFlightByIata_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            //_mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(null);
            throw new NotImplementedException("Implement this test");

            //Act
            var result = _controller.GetFlightByIata("test");

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}
