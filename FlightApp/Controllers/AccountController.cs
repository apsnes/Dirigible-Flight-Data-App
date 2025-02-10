
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
                return Unauthorized(result);
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
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserDetailsByEmail(string email)
        {
            try
            {            
                string userIdValue = email;
                UserDTO? user = await _accountService.GetUserDetailsByEmail(userIdValue);
                if (user == null) return NotFound();
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
        [HttpPut("{userId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateUserByAdmin([FromBody] UserDTO userDto, string userId)
        {
            try
            {
                string userIdValue = userId;
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
            
            if (!ModelState.IsValid || model.Password!=model.ConfirmPassword)
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

            if (!ModelState.IsValid || model.Password!=model.ConfirmPassword)
            {
                return BadRequest(ModelState);
            }
            string? userId = User.FindFirst("Id").Value;
            if (userId!=null)
            {
                var result = await _accountService.UpdatePassword(userId, model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
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

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateUserRoles([FromBody] RolesUpdateDto roleUpdate)
        {
            try
            {
                var result = await _accountService.UpdateUserRoles(roleUpdate.Role, roleUpdate.UserId);
                if (result.IsSuccess == true)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserDetailsByUserId(string userId)
        {
            try
            {
                UserDTO? user = await _accountService.GetUserDetails(userId);
                if (user == null) return BadRequest();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
