using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Models.Client;
using TheBikeSupplier.Models.Client.Authorize;
using TheBikeSupplier.Web.Client.Services.Interfaces;

namespace TheBikeSupplier.Web.Client.Services.Implementations
{
    public class AuthorizeApiService : IAuthorizeApiService
    {
        private readonly HttpClient _httpClient;

        public AuthorizeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OperationResult> Register(RegisterParameters registerParameters)
        {
            var response = await _httpClient.PostAsJsonAsync($"Authorize/Register", registerParameters);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return OperationResult.CreateFailure(message.Detail);
            }

            return OperationResult.CreateSuccess();
        }

        public async Task<OperationResult> Login(LoginParameters loginParameters)
        {
            var response = await _httpClient.PostAsJsonAsync($"Authorize/Login", loginParameters);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return OperationResult.CreateFailure(message.Detail);
            }
            return OperationResult.CreateSuccess();
        }

        public async Task Logout()
        {
            var response = await _httpClient.PostAsync("Authorize/Logout", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<UserInfo> GetUserInfo()
        {
            var user = await _httpClient.GetFromJsonAsync<UserInfo>($"Authorize/GetUserInfo");
            return user;
        }
    }
}
