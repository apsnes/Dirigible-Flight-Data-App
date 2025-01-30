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
            FlightResponseWrapper wrapper = null;
            _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(wrapper);

            //Act
            var result = _controller.GetFlightByIata("test");

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void GetFlightByIata_InvokesServiceOnce()
        {
            //Arrange
            //Act
            _controller.GetFlightByIata("test");

            //Assert
            _mockService.Verify(x => x.GetFlightByIata("test"), Times.Once);
        }

        [Test]
        public void GetFlightsByArrivalIataActive_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightsByArrivalIataActive(It.IsAny<string>())).Returns(new List<FlightResponse>());

            //Act
            var result = _controller.GetFlightsByArrivalIataActive("test");

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetFlightsByArrivalIataActive_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            List<FlightResponse> List = null;
            _mockService.Setup(x => x.GetFlightsByArrivalIataActive(It.IsAny<string>())).Returns(List);

            //Act
            var result = _controller.GetFlightsByArrivalIataActive("test");

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void GetFlightsByArrivalIataActive_InvokesServiceOnce()
        {
            //Arrange
            //Act
            _controller.GetFlightsByArrivalIataActive("test");

            //Assert
            _mockService.Verify(x => x.GetFlightsByArrivalIataActive("test"), Times.Once);
        }

        [Test]
        public void GetIncidentFlights_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.GetIncidentFlights()).Returns(new List<FlightResponse>());

            //Act
            var result = _controller.GetIncidentFlights();

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetIncidentFlights_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            List<FlightResponse> List = null;
            _mockService.Setup(x => x.GetIncidentFlights()).Returns(List);

            //Act
            var result = _controller.GetIncidentFlights();

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void GetIncidentFlights_InvokesServiceOnce()
        {
            //Arrange
            //Act
            _controller.GetIncidentFlights();

            //Assert
            _mockService.Verify(x => x.GetIncidentFlights(), Times.Once);
        }
    }
}
