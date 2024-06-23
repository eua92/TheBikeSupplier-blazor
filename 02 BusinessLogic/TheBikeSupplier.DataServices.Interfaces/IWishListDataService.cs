using System;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.DataServices.Interfaces
{
    public interface IWishListDataService
    {
        Task<OperationResult<Guid>> GetWishListId(Guid ownerId, bool isAuthenticated);
        Task<OperationResult<WishListViewModel>> GetWishList(Guid wishListId);
        Task<OperationResult<Guid>> CreateWishList(Guid bikeId, Guid ownerId, bool isAuthenticated);
        Task<OperationResult<Guid>> UpdateWishList(Guid bikeId, Guid wishListId);
    }
}
