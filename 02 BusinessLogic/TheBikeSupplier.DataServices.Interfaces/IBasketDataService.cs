using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.Models.Client;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.DataServices.Interfaces
{
    public interface IBasketDataService
    {
        Task<OperationResult<BasketViewModel>> GetBasketItems(Guid basketId);
        Task<OperationResult<BasketInfo>> GetBasketInfo(Guid ownerId, bool isAuthenticated);
        Task<OperationResult<BasketInfo>> CreateBasket(Guid bikeId, Guid ownerId, bool isAuthenticated);
        Task<OperationResult<BasketInfo>> UpdateBasket(Guid bikeId, Guid basketId);
        Task<OperationResult> AddBasketItems(Guid basketId, Guid userId);
        Task<OperationResult> RemoveBasketItem(Guid basketId, Guid basketItemId);
        Task<OperationResult> UpdateBasketItemQuantity(Guid basketItemId, int newQuantity);
        Task<OperationResult> UpdateBasketStatus(Guid basketId, BasketStatus newStatus);
        Task<OperationResult> SetAbandonedBasketStatus(DateTime abandonmentDate);
        Task<OperationResult> SetExpiredBasketStatus(DateTime expirationDate);
    }
}
