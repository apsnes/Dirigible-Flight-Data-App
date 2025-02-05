
using AutoMapper;
using FlightApp.Helpers;
using FlightApp.Models;
using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightApp.Service
{

    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApiAuthenticationSettings _authSettings;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<ApiAuthenticationSettings> authSettings,
            IMapper mapper
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authSettings = authSettings.Value;
            _mapper = mapper;
        }

        public async Task<SignUpResponseDTO> Register(SignUpRequestDTO signUpRequestDTO)
        {

            var user = new ApplicationUser()
            {
                UserName = signUpRequestDTO.Email,
                Email = signUpRequestDTO.Email,
                FirstName = signUpRequestDTO.FirstName,
                LastName = signUpRequestDTO.LastName,
                PhoneNumber = signUpRequestDTO.PhoneNumber,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, signUpRequestDTO.Password);
            if (!result.Succeeded)
            {
                return new SignUpResponseDTO()
                {
                    IsRegistrationSuccessful = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }
            var roleResult = await _userManager.AddToRoleAsync(user, StaticRoles.Customer);
            if (!roleResult.Succeeded)
            {
                return new SignUpResponseDTO()
                {
                    IsRegistrationSuccessful = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }
            return new SignUpResponseDTO()
            {
                IsRegistrationSuccessful = true
            };
        }
        public async Task<SignInResponseDTO> SignIn(SignInRequestDTO signInRequestDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.UserName, signInRequestDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInRequestDTO.UserName);
                if (user == null)
                {
                    return new SignInResponseDTO
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Invalid Authentication"
                    };
                }
                //everything is valid and we need to login the user
                var signinCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _authSettings.ValidIssuer,
                    audience: _authSettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signinCredentials
                    );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return new SignInResponseDTO
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    UserDTO = new UserDTO
                    {
                        Id = user.Id,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Pronouns = user.Pronouns,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        DisplayName = user.DisplayName,
                        Karma = user.Karma

                    }
                };


            }
            else
            {
                return new SignInResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                };
            }


        }
        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id)
            };
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_authSettings.SecretKey);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<UserDTO> GetUserDetails(string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                UserDTO userDTO = new UserDTO();
                UserDTO userDTO = new UserDTO()
                {
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Pronouns = user.Pronouns,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DisplayName = user.DisplayName,
                    Karma = user.Karma,

                };
                return userDTO;
            }
            return null;

        }
        public async Task<ResponseItem> UpdateUser(string userId, UserDTO userDto)
        {
            try
            {


                ApplicationUser? user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {

                    user = _mapper.Map<ApplicationUser>(userDto);


                    await _userManager.UpdateAsync(user);
                    return new ResponseItem()
                    {
                        IsSuccess = true,
                        Message = "Success"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseItem()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
            return new ResponseItem()
            {
                IsSuccess = false,
                Message = "Something went wrong"
            };


        }

        public async Task<ResponseItem> ResetPassword(PasswordResetDto dto)
        {
            ResponseItem responseItem = new ResponseItem()
            {
                IsSuccess = false,
                Message = ""
            };
            try
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                {

                    responseItem.Message = "User not found";
                    return responseItem;

                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, dto.Password);
                if (result.Succeeded)
                {
                    responseItem.IsSuccess = true;
                    responseItem.Message = "Success";
                    return responseItem;

                }
            }
            catch (Exception ex)
            {

                responseItem.Message = ex.Message;
                return responseItem;

            }

            responseItem.Message = "an error occurred";
            return responseItem;
        }
        public async Task<ResponseItem> UpdatePassword(string userId, PasswordUpdateDto dto)
        {
            ResponseItem responseItem = new ResponseItem()
            {
                IsSuccess = false,
                Message = ""
            };
            try
            {
                ApplicationUser? user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {

                    responseItem.Message = "User not found";
                    return responseItem;

                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, dto.Password);
                if (result.Succeeded)
                {
                    responseItem.IsSuccess = true;
                    responseItem.Message = "Success";
                    return responseItem;

                }
            }
            catch (Exception ex)
            {

                responseItem.Message = ex.Message;
                return responseItem;

            }

            responseItem.Message = "an error occurred";
            return responseItem;
        }
        public async Task<ResponseItem> AssignRoleToUser(string email, string role)
        {
            ResponseItem responseItem = new ResponseItem();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                responseItem.IsSuccess = false;
                responseItem.Message = "User not found";
                return responseItem;

            }

            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                responseItem.IsSuccess = false;
                responseItem.Message = "Role not found";
                return responseItem;
            }

            var result = await _userManager.AddToRoleAsync(user, role.ToString());
            if (!result.Succeeded)
            {
                responseItem.IsSuccess = false;
                responseItem.Message = "Something went wrong";

            }

            responseItem.IsSuccess = true;
            responseItem.Message = "Success";
            return responseItem;

        }

    }
}