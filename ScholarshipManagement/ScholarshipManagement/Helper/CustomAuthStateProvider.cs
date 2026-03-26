using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ScholarshipManagement.Helper
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly LocalStorageService _localStorage;

        public CustomAuthStateProvider(LocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetToken();

            if (string.IsNullOrWhiteSpace(token))
                return Anonymous();

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var identity = new ClaimsIdentity(jwt.Claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch
            {
                await _localStorage.RemoveToken();
                return Anonymous();
            }
        }

        private static AuthenticationState Anonymous()
            => new(new ClaimsPrincipal(new ClaimsIdentity()));



        public void NotifyLogin()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void NotifyLogout()
        {
            NotifyAuthenticationStateChanged(
                Task.FromResult(
                    new AuthenticationState(
                        new ClaimsPrincipal(new ClaimsIdentity())
                    )
                )
            );
        }


    }
}
