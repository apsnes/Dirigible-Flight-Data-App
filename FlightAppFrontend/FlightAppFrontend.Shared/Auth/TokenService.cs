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
        private readonly IJsInteropService _jsInteropService;
        private readonly TokenStateService _tokenStateService;
        private string _token = null;

        public TokenService(IJsInteropService jsInteropService, TokenStateService tokenStateService)
        {
            _jsInteropService = jsInteropService;
            _tokenStateService = tokenStateService;
        }

        public async Task<string> LoadTokenAsync()
        {
            if (_token == null)
            {
                _token = await GetTokenFromLocalStorageAsync();
                await _tokenStateService.SetTokenAsync(_token);
            }
            return _token;
        }

        public async Task<string> GetTokenAsync()
        {
            if (_token == null)
            {
                return await _tokenStateService.GetTokenAsync();
            }
            return _token;
        }

        private async Task<string> GetTokenFromLocalStorageAsync()
        {
            if (_token == null)
            {
                var token = await _jsInteropService.GetItem("jwtToken");
                return token;
            }
            return _token;
        }

        public async Task SetTokenToNullAsync()
        {
            await _jsInteropService.SetItem("jwtToken", null);
            _token = null;
        }

        public async Task RemoveTokenAsync()
        {
            await _jsInteropService.RemoveItem("jwtToken");
            _token = null;
        }
    }



}
