using Blazored.LocalStorage;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.Models.Client;
using TheBikeSupplier.Models.Client.Dtos;
using TheBikeSupplier.Models.Client.ViewModels;
using TheBikeSupplier.Web.Client.Services.Interfaces;

namespace TheBikeSupplier.Web.Client.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IUserService _userService;
        private readonly IToasterService _toasterService;
        private readonly HttpClient _httpClient;

        public event Action OnChange;
        public BasketViewModel Basket { get; set; } = new();
        public BasketInfo BasketInfo { get; set; } = new();

        public BasketService(IUserService userService, IToasterService toasterService, HttpClient httpClient)
        {
            _userService = userService;
            _toasterService = toasterService;
            _httpClient = httpClient;
        }

        public async Task AddBasketItem(Guid bikeId, ClaimsPrincipal currentUser)
        {
            HttpResponseMessage response;
            if (BasketInfo.Id == null)
                response = await _httpClient.PostAsJsonAsync($"Basket/CreateBasket/{bikeId}", await _userService.CreateUserDataDto(currentUser));
            else
                response = await _httpClient.PostAsJsonAsync($"Basket/UpdateBasket/{bikeId}", BasketInfo.Id);
           
            if (response.IsSuccessStatusCode)
            {
                BasketInfo = await response.Content.ReadFromJsonAsync<BasketInfo>();
            }
            OnChange.Invoke();
        }

        public async Task GetBasketInfo(ClaimsPrincipal currentUser)
        {
            var response = await _httpClient.PostAsJsonAsync("Basket/GetBasketInfo", await _userService.CreateUserDataDto(currentUser));
            if (response.IsSuccessStatusCode)
            {
                BasketInfo = await response.Content.ReadFromJsonAsync<BasketInfo>();
            }
        }

        public async Task GetBasketItems()
        {
            if (BasketInfo.Id != null)
            {
                var response = await _httpClient.GetAsync($"Basket/GetBasketItems/{BasketInfo.Id}");
                if (response.IsSuccessStatusCode)
                {
                    Basket = await response.Content.ReadFromJsonAsync<BasketViewModel>();
                }
                BasketInfo.Id = Basket.Id != Guid.Empty ? Basket.Id : null;
                BasketInfo.Count = Basket.TotalItems;
                OnChange?.Invoke();
            }         
        }

        public async Task RemoveBasketItem(Guid basketId, Guid basketItemId)
        {
            var response = await _httpClient.DeleteAsync($"Basket/RemoveBasketItem/{basketId}/{basketItemId}");
            if (response.IsSuccessStatusCode)
            {
                await GetBasketItems();
            }
        }

        public async Task AddBasketItems(ClaimsPrincipal currentUser)
        {
            if (BasketInfo.Id != null)
            {
                var response = await _httpClient.PostAsJsonAsync($"Basket/AddBasketItems/{BasketInfo.Id}", await _userService.CreateUserDataDto(currentUser));
                if (response.IsSuccessStatusCode)
                {
                    await GetBasketItems();
                }
            }
            OnChange.Invoke();
        }

        public async Task UpdateBasketItemQuantity(Guid basketItemId, int newQuantity)
        {
            var response = await _httpClient.PostAsJsonAsync($"Basket/UpdateBasketItemQuantity/{basketItemId}", newQuantity);
            if (response.IsSuccessStatusCode)
            {
                await GetBasketItems();
            }
        }

        public async Task UpdateBasketStatus(BasketStatus newStatus)
        {
            var response = await _httpClient.PostAsJsonAsync($"Basket/UpdateBasketStatus/{Basket.Id}", newStatus);
        }    
    }
}
