using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.Models.Client;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.Web.Client.Services.Interfaces
{
    public interface IBasketService
    {
        event Action OnChange;
        Task AddBasketItem(Guid bikeId, ClaimsPrincipal currentUser);
        Task GetBasketInfo(ClaimsPrincipal currentUser);
        Task GetBasketItems();
        Task RemoveBasketItem(Guid basketId, Guid basketItemId);
        Task AddBasketItems(ClaimsPrincipal currentUser);
        Task UpdateBasketItemQuantity(Guid basketItemId, int newQuantity);
        Task UpdateBasketStatus(BasketStatus newStatus);
        BasketViewModel Basket { get; set; }
        BasketInfo BasketInfo { get; set; }
    }
}
