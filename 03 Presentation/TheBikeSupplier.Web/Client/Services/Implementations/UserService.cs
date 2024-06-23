using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.Models.Client.Dtos;
using TheBikeSupplier.Models.Client.ViewModels;
using TheBikeSupplier.Web.Client.Services.Interfaces;

namespace TheBikeSupplier.Web.Client.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public AppUserViewModel CurrentUser { get; set; } = new();

        public UserService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task GetCurrentUser()
        {
            var response = await _httpClient.GetAsync($"User/GetCurrentUser");
            if (response.IsSuccessStatusCode)
            {
                CurrentUser = await response.Content.ReadFromJsonAsync<AppUserViewModel>();
            }
        }

        public async Task<UserDataDto> CreateUserDataDto(ClaimsPrincipal currentUser)
        {
            var isAuthenticated = currentUser.Identity.IsAuthenticated;
            return new UserDataDto
            {
                OwnerId = isAuthenticated ? GetUserId(currentUser) : await GetOrSetVisitorId(),
                IsAuthenticated = isAuthenticated
            };
        }

        private Guid GetUserId(ClaimsPrincipal currentUser)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier);
            Guid.TryParse(userId?.Value, out Guid userIdGuid);
            return userIdGuid;
        }

        private async Task<Guid> GetOrSetVisitorId()
        {
            var visitorId = await _localStorageService.GetItemAsync<Guid>("visitorId");
            if (visitorId != Guid.Empty)
                return visitorId;

            await _localStorageService.SetItemAsync("visitorId", Guid.NewGuid());
            return await _localStorageService.GetItemAsync<Guid>("visitorId");
        }
    }    
}
