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
    public class FlightServiceTests
    {
        private Mock<IFlightsRepository> _mockRepository;
        private FlightsService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IFlightsRepository>();
            _service = new FlightsService(_mockRepository.Object);
        }
        [Test]
        public void GetFlights_Invokes_Once()
        {
            //Arrange
            //Act
            _service.GetFlights();

            //Assert
            _mockRepository.Verify(x => x.GetFlights(), Times.Once);
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
        [Test]
        public void AddFlight_Invokes_Once()
        {
            //Arrange
            //Act
            _service.AddFlight(new Flight());

            //Assert
            _mockRepository.Verify(x => x.AddFlight(It.IsAny<Flight>()), Times.Once);
        }
    }
}
