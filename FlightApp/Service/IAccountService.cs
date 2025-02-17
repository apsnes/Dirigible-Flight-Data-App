﻿using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Response;

namespace FlightApp.Service
{
    public interface IAccountService
    {
        Task<UserDTO> GetUserDetails(string userId);
        Task<SignUpResponseDTO> Register(SignUpRequestDTO signUpRequestDTO);
        Task<SignInResponseDTO> SignIn(SignInRequestDTO signInRequestDTO);
        Task<ResponseItem> ResetPassword(PasswordResetDto dto);
        Task<ResponseItem> UpdateUser(string userId, UserDTO userDto);
        Task<ResponseItem> UpdatePassword(string userId, PasswordUpdateDto dto);
        Task<ResponseItem> AssignRoleToUser(string email, string role);
        Task<UserDTO> GetUserDetailsByEmail(string email);
        Task<ResponseItem> UpdateUserRoles(string role, string userId);
    }
}