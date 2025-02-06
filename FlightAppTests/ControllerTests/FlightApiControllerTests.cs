using FlightApp.Controllers;
using FlightAppLibrary.Models.Response;
using FlightApp.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace FlightAppTests.ControllerTests
{
    public class FlightApiServiceTests
    {
        private Mock<IFlightService> _mockService;
        private FlightApiController _controller;
        private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        
        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IFlightService>();
            _controller = new FlightApiController(_mockService.Object, _cache);
        }

        [Test]
        public void GetFlightByIata_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(new FlightResponse());

            //Act
            var result = _controller.GetFlightByIata("test1");

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
            var result = _controller.GetFlightByIata("test2");

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetFlightByIata_InvokesServiceOnce()
        {
            //Arrange
            //Act
            _controller.GetFlightByIata("test3");

            //Assert
            _mockService.Verify(x => x.GetFlightByIata("test3"), Times.Once);
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

        [Test]
        public void GetFlightByIata_InvokesOnceForIdenticalRequests()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(new FlightResponse());

            //Act
            _controller.GetFlightByIata("BA001");
            _controller.GetFlightByIata("BA001");



            //Assert
            _mockService.Verify(x => x.GetFlightByIata(It.IsAny<string>()), Times.Once);

        }

        [Test]
        public void GetFlightByIata_InvokesTwiceForDifferentRequests()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(new FlightResponse());

            //Act
            _controller.GetFlightByIata("BA002");
            _controller.GetFlightByIata("BA003");



            //Assert
            _mockService.Verify(x => x.GetFlightByIata(It.IsAny<string>()), Times.Exactly(2));

        }

        [Test]
        public void GetSearchResults_InvokesOnceForIdenticalRequests()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightsByDepartureAirportActive(It.IsAny<string>())).Returns(new List<FlightResponse>());

            //Act
            _controller.GetSearchResults(null,departures: "LHR", null, null, page_number: 0, page_size: 0);
            _controller.GetSearchResults(null, departures: "LHR", null, null, page_number: 0, page_size: 0);
            _controller.GetSearchResults(null, departures: "LHR", null, null, page_number: 0, page_size: 0);
            _controller.GetSearchResults(null, departures: "LHR", null, null, page_number: 0, page_size: 0);


            //Assert
            _mockService.Verify(x => x.GetFlightsByDepartureAirportActive(It.IsAny<string>()), Times.Once);
            _mockService.Verify(x => x.GetFlightsByArrivalIataActive(It.IsAny<string>()), Times.Never);

        }

        [Test]
        public void GetSearchResults_InvokesTwiceForDifferentRequests()
        {
            //Arrange
            _mockService.Setup(x => x.GetFlightsByDepartureAirportActive(It.IsAny<string>())).Returns(new List<FlightResponse>());

            //Act
            _controller.GetSearchResults(null, "MAN", null, null, 0, 0);
            _controller.GetSearchResults(null, "DFW", null, null, 0, 0);


            //Assert
            _mockService.Verify(x => x.GetFlightsByDepartureAirportActive(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void GetIncidetFlights_InvokesAtMostOnceForIdenticalRequests()
        {
            //Arrange
            _mockService.Setup(x => x.GetIncidentFlights());

            //Act
            _controller.GetIncidentFlights();
            _controller.GetIncidentFlights();
            _controller.GetIncidentFlights();
            _controller.GetIncidentFlights();


            //Assert
            _mockService.Verify(x => x.GetIncidentFlights(), Times.AtMost(1));

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _cache.Dispose();
        }
    }
}
