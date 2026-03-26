using Microsoft.JSInterop;
using static ScholarshipManagement.Helper.Constant;

namespace ScholarshipManagement.Helper
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _js;

        public LocalStorageService(IJSRuntime js)
        {
            _js = js;
        }

        // JWT token methods -- at a time only one token would be there 
        // Ensures only ONE active token at a time.
        // When storing in one storage, the other is cleared to avoid conflicts.


        // Remember Me = TRUE → localStorage → Save permanently
        public async Task SetTokenPersistent(string token)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", LocalStorageKeys.Token, token);
            await _js.InvokeVoidAsync("sessionStorage.removeItem", LocalStorageKeys.Token);
        }


        // Remember Me = FALSE → sessionStorage → Save temporarily
        public async Task SetTokenSession(string token)
        {
            await _js.InvokeVoidAsync("sessionStorage.setItem", LocalStorageKeys.Token, token);
            await _js.InvokeVoidAsync("localStorage.removeItem", LocalStorageKeys.Token);
        }



        // Auto-detect token from either storage
        // Always prefer session token (active role) over persistent token
        public async Task<string?> GetToken()
        {
            // Always check session first (current active token)
            var session = await _js.InvokeAsync<string>("sessionStorage.getItem", LocalStorageKeys.Token);
            if (!string.IsNullOrWhiteSpace(session))
                return session;

            // fallback to persistent token
            return await _js.InvokeAsync<string>("localStorage.getItem", LocalStorageKeys.Token);
        }

        // Clear token from both storages
        public async Task RemoveToken()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", LocalStorageKeys.Token);
            await _js.InvokeVoidAsync("sessionStorage.removeItem", LocalStorageKeys.Token);
        }






        // Generic local storage methods for user info
        // (User info still stays in localStorage — OK for development)

        public async Task SetItem(string key, string value) =>
            await _js.InvokeVoidAsync("localStorage.setItem", key, value);

        public async Task<string?> GetItem(string key) =>
            await _js.InvokeAsync<string>("localStorage.getItem", key);

        public async Task RemoveItem(string key) =>
            await _js.InvokeVoidAsync("localStorage.removeItem", key);
    }
}
