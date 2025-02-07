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
        public IActionResult PostVote([FromBody] VoteDto voteDto)
        {
            if(User != null)
            {
                var userId = User.FindFirst("Id");
                string userIdValue = userId!.Value;
                var result = _votesService.AddVote(voteDto, userIdValue);
                return result == null ? BadRequest($"Unable to vote") : Ok(result);
            }
            return BadRequest($"User info may not be correct");
        }
    }
}
