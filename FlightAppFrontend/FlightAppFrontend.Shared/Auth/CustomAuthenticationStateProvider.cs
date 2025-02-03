using FlightAppFrontend.Shared.Services;
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
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly TokenStateService _tokenStateService;
    
        

        public CustomAuthenticationStateProvider(TokenStateService tokenStateService)
        {
            _tokenStateService = tokenStateService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = _tokenStateService.GetToken();
            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();

                if (handler.CanReadToken(token))
                {
                    var jwtToken = handler.ReadJwtToken(token);
                    var claims = jwtToken.Claims.ToList();

                    // Add additional claims if necessary
                    claims.Add(new Claim("Token", token));

                    identity = new ClaimsIdentity(claims, "jwtAuthType");
                }
                else
                {
                    Console.WriteLine("Invalid token format");
                }
            }

            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));

        }

        public void NotifyAuthenticationStateChanged()
        {
            var authState = GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(authState);
        }
    }


}
