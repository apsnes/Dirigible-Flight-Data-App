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
        private AuthenticationState _anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public CustomAuthenticationStateProvider(TokenStateService tokenStateService)
        {
            _tokenStateService = tokenStateService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenStateService.GetTokenAsync();
            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();

                if (handler.CanReadToken(token))
                {
                    var jwtToken = handler.ReadJwtToken(token);
                    if (jwtToken.ValidTo < DateTime.UtcNow)
                    {
                        Console.WriteLine("Token has expired");
                        return _anonymous;
                    }

                    var claims = jwtToken.Claims.ToList();
                    claims.ForEach(claim => Console.WriteLine($"Claim: {claim.Type} - {claim.Value}"));

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
            return new AuthenticationState(user);
        }

        public void NotifyAuthenticationStateChanged()
        {
            var authState = GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(authState);
        }
    }

}
