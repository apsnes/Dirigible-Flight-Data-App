
using FlightApp.Models;
using FlightApp.Service;
using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDTO)
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
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserDetails()
        {
            try
            {
                var userId = User.FindFirst("Id");
                string userIdValue = userId.Value;
                UserDTO? user = await _accountService.GetUserDetails(userIdValue);
                if (user == null) return BadRequest();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDto)
        {
            try
            {
                var userId = User.FindFirst("Id");
                string userIdValue = userId.Value;
                var result = await _accountService.UpdateUser(userIdValue, userDto);
                if (!result.IsSuccess) return BadRequest(result.Message);
                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountService.ResetPassword(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst("Id").Value;
            var result = await _accountService.UpdatePassword(userId, model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignmentItem roleItem)
        {
            var result = await _accountService.AssignRoleToUser(roleItem.Email, roleItem.Role);
            if (result.IsSuccess == true)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
