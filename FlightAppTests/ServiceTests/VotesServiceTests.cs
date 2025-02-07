using FlightApp.Controllers;
using FlightApp.Entities;
using FlightApp.Repository;
using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Enums;
using Moq;

namespace FlightAppTests.ServiceTests
{
    public class VotesServiceTests
    {
        private Mock<IVotesRepository> _votesRepository;
        private VotesService _service;

        [SetUp]
        public void Setup()
        {
            _votesRepository = new Mock<IVotesRepository>();
            _service = new VotesService(_votesRepository.Object);
        }

        [Test]
        public void AddVote_Invokes_Once()
        {
            //Arrange
            var vote = new VoteDto()
            {
                Value = 1,
                CommenterId = "Id",
                CommentId = 1,
                CommentType = CommentType.Note
            };

            //Act
            _service.AddVote(vote, "Id");

            //Assert
            _votesRepository.Verify(x => x.AddVote(It.IsAny<Vote>(), "Id"), Times.Once);
        }
    }
}
