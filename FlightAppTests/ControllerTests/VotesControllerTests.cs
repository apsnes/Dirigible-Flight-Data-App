using FlightApp.Controllers;
using FlightApp.Entities;
using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlightAppTests.ControllerTests
{
    public class VotesControllerTests
    {
        private Mock<IVotesService> _votesService;
        private VotesController _controller;

        [SetUp]
        public void Setup()
        {
            _votesService = new Mock<IVotesService>();
            _controller = new VotesController(_votesService.Object);
        }

        [Test]
        public void PostVote_InvalidUser_Returns_BadRequest()
        {
            //Arrange
            Vote? response = null;
            _votesService.Setup(x => x.AddVote(It.IsAny<VoteDto>(), "Id")).Returns(response);

            //Act
            var result = _controller.PostVote(new VoteDto());

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}
