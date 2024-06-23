using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.Web.Client.Services.Interfaces
{
    public interface IWishListService
    {
        Task GetWishListId(ClaimsPrincipal currentUser);
        Task GetWishList();
        Task AddWishListItem(Guid bikeId, ClaimsPrincipal currentUser);
        Guid WishListId { get; set; }
        WishListViewModel WishList { get; set; }
    }
}
