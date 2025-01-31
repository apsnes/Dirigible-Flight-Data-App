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
        public void AddNote_ValidResponse_Returns_OK()
        {
            //Arrange
            _mockService.Setup(x => x.AddNote(It.IsAny<Note>())).Returns(new Note());

            //Act
            var result = _controller.AddNote(new Note());

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void AddNote_InvalidResponse_Returns_BadRequest()
        {
            //Arrange
            Note? response = null;
            _mockService.Setup(x => x.AddNote(It.IsAny<Note>())).Returns(response);

            //Act
            var result = _controller.AddNote(new Note());

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void AddNote_InvokesServiceOnce()
        {
            //Arrange
            //Act
            _controller.AddNote(new Note());

            //Assert
            _mockService.Verify(x => x.AddNote(It.IsAny<Note>()), Times.Once);
        }
    }
}
