using FlightApp.Entities;
using FlightApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        //------GET REQUESTS------
        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _usersService.GetUsers();
            return result == null ? BadRequest("Unable to find any users") : Ok(result);
        }


        //-----POST REQUESTS-----
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            var result = _usersService.AddUser(user);
            return result == null ? BadRequest($"Unable to add user {user.Id}") : Ok(result);
        }
    }
}
