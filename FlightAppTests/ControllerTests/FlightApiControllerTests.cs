using FlightApp.Controllers;
using FlightAppLibrary.Models.Response;
using FlightApp.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlightAppTests.ControllerTests
{
    public class FlightApiServiceTests
    {
        private Mock<IFlightService> _mockService;
        private FlightApiController _controller;
        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IFlightService>();
            _controller = new FlightApiController(_mockService.Object);
        }

        [Test]
        public void GetFlightByIata_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(new FlightResponse());

            //Act
            var result = _controller.GetFlightByIata("test");

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
        [Test]
        public void GetFlightByIata_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            FlightResponse? response = null;
            _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(response);

            //Act
            var result = _controller.GetFlightByIata("test");

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
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

        [Test]
        public void GetFlightsByRoute_InvokesOnce()
        {
            //Arrange
            //Act
            _controller.GetFlightsByRoute("test", "test");

            //Assert
            _mockService.Verify(x => x.GetFlightsByRoute("test", "test"), Times.Once);
        }

        [Test]
        public void GetFlightsByDepartureAirportActive_InvokesOnce()
        {
            //Arrange
            //Act
            _controller.GetFlightsByDepartureAirportActive("test");

            //Assert
            _mockService.Verify(x => x.GetFlightsByDepartureAirportActive("test"), Times.Once);
        }

        [Test]
        public void GetFlightsByRoute_ValidResponse_ReturnsOK()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightsByRoute("test", "test")).Returns(new List<FlightResponse>());

            //Act
            var result = _controller.GetFlightsByRoute("test", "test");

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetFlightsByRoute_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            List<FlightResponse> List = null;
            _mockService.Setup(x => x.GetFlightsByRoute("test", "test")).Returns(List);

            //Act
            var result = _controller.GetFlightsByRoute("test", "test");

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void GetFlightsByDepartureAirportActive_ValidResponse_ReturnsOK()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightsByDepartureAirportActive("test")).Returns(new List<FlightResponse>());

            //Act
            var result = _controller.GetFlightsByDepartureAirportActive("test");

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetFlightsByDepartureAirportActive_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            List<FlightResponse> List = null;
            _mockService.Setup(x => x.GetFlightsByDepartureAirportActive("test")).Returns(List);

            //Act
            var result = _controller.GetFlightsByDepartureAirportActive("test");

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }
    }
}
