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
    public class NoteServiceTests
    {
        private Mock<INotesRepository> _mockRepository;
        private NotesService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<INotesRepository>();
            _service = new NotesService(_mockRepository.Object);
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
        public void AddNote_Invokes_Once()
        {
            //Arrange
            //Act
            _service.AddNote(new Note());

            //Assert
            _mockRepository.Verify(x => x.AddNote(It.IsAny<Note>()), Times.Once);
        }
    }
}
