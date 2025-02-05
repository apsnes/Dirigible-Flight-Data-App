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
    //public class FlightControllerTests
    //{
    //    private Mock<IFlightsService> _mockService;
    //    private FlightsController _controller;

    //    [SetUp]
    //    public void Setup()
    //    {
    //        _mockService = new Mock<IFlightsService>();
    //        _controller = new FlightsController(_mockService.Object);
    //    }

    //    [Test]
    //    public void GetFlights_ValidResponse_Returns_OK()
    //    {
    //        //Arrange
    //        _mockService.Setup(x => x.GetFlights()).Returns(new List<Flight>());

    //        //Act
    //        var result = _controller.GetFlights();

    //        //Assert
    //        Assert.That(result, Is.TypeOf<OkObjectResult>());
    //    }

    //    [Test]
    //    public void GetFlight_InvalidResponse_Returns_BadRequest()
    //    {
    //        //Arrange
    //        Flight? response = null;
    //        _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(response);

    //        //Act
    //        var result = _controller.GetFlightByIata("test");

    //        //Assert
    //        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
    //    }

    //    [Test]
    //    public void GetFlight_InvokesServiceOnce()
    //    {
    //        //Arrange
    //        //Act
    //        _controller.GetFlightByIata("test");
    //        //Assert
    //        _mockService.Verify(x => x.GetFlightByIata("test"), Times.Once);
    //    }

    //    [Test]
    //    public void GetFlightByIata_ValidResponse_Returns_OK()
    //    {
    //        //Arrange
    //        _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(new Flight());

    //        //Act
    //        var result = _controller.GetFlightByIata("test");

    //        //Assert
    //        Assert.That(result, Is.TypeOf<OkObjectResult>());
    //    }

    //    [Test]
    //    public void GetFlightByIata_InvalidResponse_Returns_BadRequest()
    //    {
    //        //Arrange
    //        Flight? response = null;
    //        _mockService.Setup(x => x.GetFlightByIata(It.IsAny<string>())).Returns(response);

    //        //Act
    //        var result = _controller.GetFlightByIata("test");

    //        //Assert
    //        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
    //    }

    //    [Test]
    //    public void GetFlightByIata_InvokesServiceOnce()
    //    {
    //        //Arrange
    //        //Act
    //        _controller.GetFlightByIata("test");

    //        //Assert
    //        _mockService.Verify(x => x.GetFlightByIata("test"), Times.Once);
    //    }

    //    [Test]
    //    public void AddFlight_ValidResponse_Returns_OK()
    //    {
    //        //Arrange
    //        _mockService.Setup(x => x.AddFlight(It.IsAny<Flight>())).Returns(new Flight());

    //        //Act
    //        var result = _controller.AddFlight(new Flight());

    //        //Assert
    //        Assert.That(result, Is.TypeOf<OkObjectResult>());
    //    }

    //    [Test]
    //    public void AddFlight_InvalidResponse_Returns_BadRequest()
    //    {
    //        //Arrange
    //        Flight? response = null;
    //        _mockService.Setup(x => x.AddFlight(It.IsAny<Flight>())).Returns(response);

    //        //Act
    //        var result = _controller.AddFlight(new Flight());

    //        //Assert
    //        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
    //    }

    //    [Test]
    //    public void AddFlight_InvokesServiceOnce()
    //    {
    //        //Arrange
    //        //Act
    //        _controller.AddFlight(new Flight());

    //        //Assert
    //        _mockService.Verify(x => x.AddFlight(It.IsAny<Flight>()), Times.Once);
    //    }
    //}
}
