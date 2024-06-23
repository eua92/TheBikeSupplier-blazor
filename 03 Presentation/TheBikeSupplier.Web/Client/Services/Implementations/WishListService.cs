using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.Models.Client.Dtos;
using TheBikeSupplier.Models.Client.ViewModels;
using TheBikeSupplier.Web.Client.Services.Interfaces;

namespace TheBikeSupplier.Web.Client.Services.Implementations
{
    public class WishListService : IWishListService
    {
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;
        public Guid WishListId { get; set; }
        public WishListViewModel WishList { get; set; } = new();

        public WishListService(IUserService userService, HttpClient httpClient)
        {
            _userService = userService;
            _httpClient = httpClient;
        }

        public async Task GetWishListId(ClaimsPrincipal currentUser)
        {
            var response = await _httpClient.PostAsJsonAsync("WishList/GetWishListId", await _userService.CreateUserDataDto(currentUser));
            if (response.IsSuccessStatusCode)
            {
                WishListId = await response.Content.ReadFromJsonAsync<Guid>();
            }
        }

        public async Task GetWishList()
        {
            if (WishListId != Guid.Empty)
            {
                var response = await _httpClient.GetAsync($"WishList/GetWishList/{WishListId}");
                if (response.IsSuccessStatusCode)
                {
                    WishList = await response.Content.ReadFromJsonAsync<WishListViewModel>();
                }
            }
        }

        public async Task AddWishListItem(Guid bikeId, ClaimsPrincipal currentUser)
        {
            HttpResponseMessage response;
            if (WishListId == Guid.Empty)
                response = await _httpClient.PostAsJsonAsync($"WishList/CreateWishList/{bikeId}", await _userService.CreateUserDataDto(currentUser));
            else
                response = await _httpClient.PostAsJsonAsync($"WishList/UpdateWishList/{bikeId}", WishListId);

            if (response.IsSuccessStatusCode)
            {
                WishListId = await response.Content.ReadFromJsonAsync<Guid>();
            }
        }       
    }
}
