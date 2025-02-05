using FlightApp.Controllers;
using FlightApp.Entities;
using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Primitives;

namespace FlightAppTests.ControllerTests
{
    public class NoteControllerTests
    {
        private Mock<INotesService> _mockService;
        private NotesController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<INotesService>();
            _controller = new NotesController(_mockService.Object);
        }

        [Test]
        public void GetNotes_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.GetNotes()).Returns(new List<Note>());

            //Act
            var result = _controller.GetNotes();

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetNotes_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            List<Note>? response = null;
            _mockService.Setup(x => x.GetNotes()).Returns(response);

            //Act
            var result = _controller.GetNotes();

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetNotes_InvokesServiceOnce()
        {
            //Arrange
            //Act
            _controller.GetNotes();
            //Assert
            _mockService.Verify(x => x.GetNotes(), Times.Once);
        }

        [Test]
        public void GetNotesByIataAndDateTime_ValidResponse_Returns_OK()
        {
            // Arrange
            var queryParams = new Dictionary<string, StringValues>
            {
                { "flight_iata", "ABC123" },
                { "departure_time", "2024-02-01T12:00:00Z" }
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Query = new QueryCollection(queryParams);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            _mockService.Setup(x => x.GetNotesByIataAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(new List<NoteDto>());

            // Act
            var result = _controller.GetNotesByIataAndDateTime();

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetNotesByIataAndDateTime_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            List<NoteDto>? response = null;
            var queryParams = new Dictionary<string, StringValues>
            {
                { "flight_iata", "ABC123" },
                { "departure_time", "2024-02-01T12:00:00Z" }
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Query = new QueryCollection(queryParams);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            _mockService.Setup(x => x.GetNotesByIataAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(response);

            // Act
            var result = _controller.GetNotesByIataAndDateTime();

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetNotesByIataAndDateTime_InvokesServiceOnce()
        {
            //Arrange
            var queryParams = new Dictionary<string, StringValues>
            {
                { "flight_iata", "ABC123" },
                { "departure_time", "2024-02-01T12:00:00Z" }
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Query = new QueryCollection(queryParams);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _controller.GetNotesByIataAndDateTime();

            // Assert
            _mockService.Verify(x => x.GetNotesByIataAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public void AddNote_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.AddNote(It.IsAny<NoteDto>())).Returns(new Note());

            //Act
            var result = _controller.AddNote(new NoteDto());

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void AddNote_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            Note? response = null;
            _mockService.Setup(x => x.AddNote(It.IsAny<NoteDto>())).Returns(response);

            //Act
            var result = _controller.AddNote(new NoteDto());

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void AddNote_InvokesServiceOnce()
        {
            //Arrange
            //Act
            _controller.AddNote(new NoteDto());

            //Assert
            _mockService.Verify(x => x.AddNote(It.IsAny<NoteDto>()), Times.Once);
        }
    }
}
