
using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]SignUpRequestDTO signUpRequestDTO)
        {
            if (signUpRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            SignUpResponseDTO result = await _accountService.Register(signUpRequestDTO);
            if (result.IsRegistrationSuccessful == false)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]SignInRequestDTO signInRequestDTO) 
        {
            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            SignInResponseDTO result = await _accountService.SignIn(signInRequestDTO);
            if (result.IsAuthSuccessful == false)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
