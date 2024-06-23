using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.EF;
using TheBikeSupplier.Models;
using TheBikeSupplier.Models.Client;

namespace TheBikeSupplier.DataServices.Implementations
{
    public class TourDataService : ITourDataService
    {
        private readonly IDbContextFactory<TheBikeSupplierContext> _dbContextFactory;
        private readonly IProductImageStorage<Tour, TourViewModel> _productImageStorage;

        public TourDataService(IDbContextFactory<TheBikeSupplierContext> dbContextFactory, IProductImageStorage<Tour, TourViewModel> productImageStorage)
        {
            _dbContextFactory = dbContextFactory;
            _productImageStorage = productImageStorage;
        }

        public async Task<OperationResult<List<TourViewModel>>> GetTours()
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var data = await context.Tours.AsNoTracking().Select(x => new TourViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    DurationType = x.DurationType,
                    Duration = x.Duration,
                    Level = x.Level,
                    MaximumPersons = x.MaximumPersons,
                    Distance = x.Distance,
                    ImageSrc = x.ImageSrc,
                    DistanceUnit = x.DistanceUnit
                }).ToListAsync();

                return OperationResult.CreateSuccess(data);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<List<TourViewModel>>(ex.Message);
            }          
        }

        public async Task<OperationResult<TourViewModel>> GetTour(Guid id)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var tour = await context.Tours.Where(x => x.Id == id).Select(x => new TourViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    DurationType = x.DurationType,
                    Duration = x.Duration,
                    Level = x.Level,
                    MaximumPersons = x.MaximumPersons,
                    Distance = x.Distance,
                    ImageSrc = x.ImageSrc,
                    DistanceUnit = x.DistanceUnit
                }).FirstOrDefaultAsync();

                return OperationResult.CreateSuccess(tour);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<TourViewModel>(ex.Message);
            }          
        }

        public async Task<OperationResult> CreateTour(TourViewModel vm)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var data = new Tour
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Price = vm.Price,
                    DurationType = vm.DurationType,
                    Duration = vm.Duration,
                    Level = vm.Level,
                    MaximumPersons = vm.MaximumPersons,
                    Distance = vm.Distance,
                    DistanceUnit = vm.DistanceUnit
                };

                context.Tours.Add(data);

                data.ImageSrc = (await _productImageStorage.UpdateProductImage(vm, data)).Result;
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> UpdateTour(TourViewModel vm)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var entity = await context.Tours.FirstOrDefaultAsync(x => x.Id == vm.Id);
                entity.Name = vm.Name;
                entity.Price = vm.Price;
                entity.DurationType = vm.DurationType;
                entity.Duration = vm.Duration;
                entity.Level = vm.Level;
                entity.MaximumPersons = vm.MaximumPersons;
                entity.Distance = vm.Distance;
                entity.DistanceUnit = vm.DistanceUnit;
                entity.ImageSrc = (await _productImageStorage.UpdateProductImage(vm, entity)).Result;

                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> DeleteTour(Guid id)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var tour = await context.Tours.FirstOrDefaultAsync(x => x.Id == id);

                context.Tours.Remove(tour);
                await context.SaveChangesAsync();

                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> DeleteTourImage(Guid tourId)
        {
            return await _productImageStorage.DeleteProductImage(tourId);
        }
    }
}
