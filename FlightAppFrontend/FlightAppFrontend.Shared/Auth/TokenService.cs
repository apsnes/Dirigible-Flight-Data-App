using FlightAppFrontend.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppFrontend.Shared.Auth
{
    public class TokenService
    {
        private readonly TokenStateService _tokenStateService;
        private string _token = null;

        public TokenService(TokenStateService tokenStateService)
        {
            _tokenStateService = tokenStateService;
        }

        public async Task<string> LoadTokenAsync()
        {
            if (string.IsNullOrEmpty(_token))
            {
                _token = await GetTokenAsync();
                await _tokenStateService.SetTokenAsync(_token);
            }
            return _token;
        }

        public async Task<string> GetTokenAsync()
        {
            if (string.IsNullOrEmpty(_token))
            {
                _token =  await _tokenStateService.GetTokenAsync();
            }
            return _token;
        }

        public async Task RemoveTokenAsync()
        {
            await _tokenStateService.RemoveTokenAsync();
            _token = null;
        }
    }
}
