using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Models.Client;

namespace TheBikeSupplier.DataServices.Interfaces
{
    public interface ITourDataService
    {
        Task<OperationResult<List<TourViewModel>>> GetTours();
        Task<OperationResult<TourViewModel>> GetTour(Guid id);
        Task<OperationResult> CreateTour(TourViewModel vm);
        Task<OperationResult> UpdateTour(TourViewModel vm);
        Task<OperationResult> DeleteTour(Guid id);
        Task<OperationResult> DeleteTourImage(Guid tourId);
    }
}
