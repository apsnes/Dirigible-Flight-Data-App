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
        private readonly IJSRuntime _jsRuntime;
        private string _token;
        

        public TokenService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> LoadTokenAsync()
        {
            // Retrieve the JWT token from localStorage
            JsInteropService jsInteropService = new(_jsRuntime);
            _token = await GetTokenFromLocalStorageAsync();
            Console.WriteLine($"this is the token {_token}");
            return _token;
        }

        public string GetToken()
        {
            return _token;
        }
        private async Task<string> GetTokenFromLocalStorageAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwtToken");
        }
    }


}
