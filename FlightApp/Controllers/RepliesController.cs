﻿using FlightApp.Entities;
using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepliesController : ControllerBase
    {
        private readonly IRepliesService _repliesService;
        public RepliesController(IRepliesService repliesService)
        {
            _repliesService = repliesService;
        }

        //------GET REQUESTS------
        [HttpGet]
        public IActionResult GetReplies()
        {
            var result = _repliesService.GetReplies();
            return result == new List<Reply>() ? BadRequest("Unable to find any replies") : Ok(result);
        }

        [HttpGet("user/{userId")]
        public IActionResult GetRepliesByUserId(string userId)
        {
            var result = _repliesService.GetRepliesByUserId(userId);
            return result == new List<ReplyDto>() ? BadRequest($"Unable to find any replies for user {userId}") : Ok(result);
        }

        [HttpGet("flight")]
        public IActionResult GetRepliesByNoteId()
        {
            int noteId = Request.Query.ContainsKey("noteId") ? int.Parse(Request.Query["noteId"]) : 0;
            var result = _repliesService.GetRepliesByNoteId(noteId);
            return result == new List<ReplyDto>() ? BadRequest("Unable to find any notes") : Ok(result);
        }

        //-----POST REQUESTS-----
        [HttpPost]
        public IActionResult AddReply([FromBody] ReplyDto replyDto)
        {
            var result = _repliesService.PostReply(replyDto);
            return result == null ? BadRequest($"Unable to add reply") : Ok(result);
        }
    }
}
