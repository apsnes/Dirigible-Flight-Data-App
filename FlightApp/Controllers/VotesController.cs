using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService _votesService;
        public VotesController(IVotesService votesService)
        {
            _votesService = votesService;
        }

        //-----POST REQUESTS-----
        [HttpPost]
        [Authorize]
        public IActionResult AddVote([FromBody] VoteDto voteDto)
        {
            var userId = User.FindFirst("Id");
            string userIdValue = userId!.Value;
            var result = _votesService.PostVote(voteDto, userIdValue);
            return result == null ? BadRequest($"Unable to vote") : Ok(result);
        }
    }
}
