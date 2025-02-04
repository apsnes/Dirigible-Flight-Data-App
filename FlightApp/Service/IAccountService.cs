using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Response;

namespace FlightApp.Service
{
    public interface IAccountService
    {
        Task<UserDTO> GetUserDetails(string userId);
        Task<SignUpResponseDTO> Register(SignUpRequestDTO signUpRequestDTO);
        Task<SignInResponseDTO> SignIn(SignInRequestDTO signInRequestDTO);
        Task<string> UpdatePassword(PasswordResetDto dto);
        Task<ResponseItem> UpdateUser(string userId, UserDTO userDto);
    }
}