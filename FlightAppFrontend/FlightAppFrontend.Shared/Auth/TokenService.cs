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
        

        public TokenService(IJsInteropService jsInteropService, TokenStateService tokenStateService)
        {
            _jsInteropService = jsInteropService;
            _tokenStateService = tokenStateService;
        }

        public async Task<string> LoadTokenAsync()
        {
            var token = await GetTokenFromLocalStorageAsync();
            _tokenStateService.SetToken( token );
            return token;
        }

        public string GetToken()
        {
            return _tokenStateService.GetToken();
        }
        private async Task<string> GetTokenFromLocalStorageAsync()
        {
            return await _jsInteropService.GetItem("jwtToken");
          
           
        }
        public async void SetTokenToNull()
        {
            await _jsInteropService.SetItem("jwtToken", null);

        }
        public async void RemoveToken() {
            await _jsInteropService.RemoveItem("jwtToken");
        
        }
    }


}
