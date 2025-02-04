
using FlightApp.Helpers;
using FlightApp.Models;
using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<ApiAuthenticationSettings> authSettings
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authSettings = authSettings.Value;
        }

        public async Task<SignUpResponseDTO> Register(SignUpRequestDTO signUpRequestDTO)
        {
            var user = new ApplicationUser
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
                        FirstName = user.FirstName,
                        LastName = user.LastName,
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
                UserDTO userDTO = new UserDTO()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber

                };
                return userDTO;
            }
            return null;

        }
    }
}
