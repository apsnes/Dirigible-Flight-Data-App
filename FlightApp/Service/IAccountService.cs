using FlightAppLibrary.Models.Dtos;

namespace FlightApp.Service
{
    public interface IAccountService
    {
        Task<SignUpResponseDTO> Register(SignUpRequestDTO signUpRequestDTO);
        Task<SignInResponseDTO> SignIn(SignInRequestDTO signInRequestDTO);
    }
}