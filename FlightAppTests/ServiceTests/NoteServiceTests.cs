using FlightApp.Repository;
using FlightApp.Service;
using FlightApp.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FlightAppLibrary.Models.Dtos;

namespace FlightAppTests.ServiceTests
{
    public class NoteServiceTests
    {
        private Mock<INotesRepository> _mockRepository;
        private NotesService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<INotesRepository>();
            var _mockMapper = new Mock<IMapper>();
            _service = new NotesService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public void GetNotes_Invokes_Once()
        {
            //Arrange
            //Act
            _service.GetNotes();

            //Assert
            _mockRepository.Verify(x => x.GetNotes(), Times.Once);
        }

        [Test]
        public void GetNotesByIataAndDateTime_Invokes_Once()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetNotesByIataAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(new List<Note>());

            //Act
            _service.GetNotesByIataAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>());

            //Assert
            _mockRepository.Verify(x => x.GetNotesByIataAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public void AddNote_Invokes_Once()
        {
            //Arrange
            var note = new NoteDto()
            {
                FlightIata = "test",
                ScheduledDeparture = DateTime.UtcNow,
                UserId = "test",
                NoteText = "test",
                TimeStamp = DateTime.UtcNow,
                Replies = []
            };

            //Act
            _service.AddNote(note);

            //Assert
            _mockRepository.Verify(x => x.AddNote(It.IsAny<Note>()), Times.Once);
        }
    }
}
