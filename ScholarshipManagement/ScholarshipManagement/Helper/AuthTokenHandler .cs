using Microsoft.AspNetCore.Components;
using ScholarshipManagement.Services.Common;
using System.Net;
using System.Net.Http.Headers;
using static ScholarshipManagement.Helper.Constant;

namespace ScholarshipManagement.Helper
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly LocalStorageService _localStorage;
        //private readonly AuthService _authService;
        private readonly NavigationManager _nav;
        private readonly CustomAuthStateProvider _authStateProvider;


        private static bool _logoutTriggered = false;

        public AuthTokenHandler(
            LocalStorageService localStorage, NavigationManager nav, CustomAuthStateProvider authStateProvider)
        {
            _localStorage = localStorage;
            _nav = nav;
            _authStateProvider = authStateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            //return await base.SendAsync(request, cancellationToken);


            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized 
                && !_logoutTriggered 
                && request.RequestUri != null 
                && !request.RequestUri.AbsolutePath.Contains(ApiEndpoints.AuthLogin))
            {
                _logoutTriggered = true;

                await _localStorage.RemoveToken();
                _authStateProvider.NotifyLogout();
                _nav.NavigateTo(Routes.Login);
            }

            return response;
        }




        private bool IsAuthEndpoint(HttpRequestMessage request)
        {
            if (request.RequestUri == null) return false;

            var path = request.RequestUri.AbsolutePath.ToLower();

            return path.Contains("auth/login") ||
                   path.Contains("auth/register") ||
                   path.Contains("auth/refresh");
        }


    }

}
