using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.Models.Client;

namespace TheBikeSupplier.DataServices.Interfaces
{
    public interface IBikeDataService
    {
        Task<OperationResult<List<BikeViewModel>>> GetBikes(AcquisitionType acquisitionType, ClaimsPrincipal currentUser, Guid? wishListId);
        Task<OperationResult<BikeViewModel>> GetBike(Guid id);
        Task<OperationResult> CreateBike(BikeViewModel vm);
        Task<OperationResult> UpdateBike(BikeViewModel vm);
        Task<OperationResult> DeleteBike(Guid id);
        Task<OperationResult> DeleteBikeImage(Guid bikeId);
    }
}
