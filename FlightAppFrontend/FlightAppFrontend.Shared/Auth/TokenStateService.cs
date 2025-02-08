using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppFrontend.Shared.Auth
{
    public class TokenStateService
    {
        private readonly IJSRuntime _jsRuntime;
        private string _token;

        public TokenStateService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetTokenAsync()
        {
            
            if (!string.IsNullOrEmpty(_token))
            {
                return _token;
            }

            if (_jsRuntime is IJSInProcessRuntime)
            {
                _token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "TokenKey");
                return _token;
            }
            else
            {
                // Defer the call and return an empty token for now
                return string.Empty;
            }
        }

        public async Task SetTokenAsync(string token)
        {
            _token = token;
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "TokenKey", token);
        }

        public void ClearToken()
        {
            _token = string.Empty;
        }
        public async Task UpdateHeaders(HttpClient httpClient)
        {
            var token = await GetTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine($"Authorization header updated: Bearer {token}");
            }
        }
    }



}
