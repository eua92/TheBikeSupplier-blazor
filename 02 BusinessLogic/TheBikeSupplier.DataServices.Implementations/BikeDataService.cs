using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.EF;
using TheBikeSupplier.Models;
using TheBikeSupplier.Models.Client;

namespace TheBikeSupplier.DataServices.Implementations
{
    public class BikeDataService : IBikeDataService
    {
        private readonly IDbContextFactory<TheBikeSupplierContext> _dbContextFactory;
        private readonly IProductImageStorage<Bike, BikeViewModel> _productImageStorage;

        public BikeDataService(IDbContextFactory<TheBikeSupplierContext> dbContextFactory, IProductImageStorage<Bike, BikeViewModel> productImageStorage)
        {
            _dbContextFactory = dbContextFactory;
            _productImageStorage = productImageStorage;
        }

        public async Task<OperationResult<List<BikeViewModel>>> GetBikes(AcquisitionType acquisitionType, ClaimsPrincipal currentUser, Guid? wishListId)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var query = context.Bikes.AsNoTracking().Where(x => x.Type == acquisitionType);
                if (!currentUser.IsInRole(Roles.Admin))
                    query = query.Where(x => x.Stock > 0);

                var wishList = await context.WishLists.AsNoTracking().Include(x => x.WishListItems).Where(x => x.Id == wishListId).FirstOrDefaultAsync();

                var data = await query.Select(x => new BikeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Stock = x.Stock,
                    Kind = x.Kind,
                    ImageSrc = x.ImageSrc,
                    InWishList = context.WishListItems.Any(x => x.ProductId == x.Id && x.WishListId == wishListId)
                }).ToListAsync();

                foreach (var item in data)
                {
                    item.InWishList = context.WishListItems.Any(x => x.ProductId == item.Id && x.WishListId == wishListId);
                }

                return OperationResult.CreateSuccess(data);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<List<BikeViewModel>>(ex.Message);
            }
        }

        public async Task<OperationResult<BikeViewModel>> GetBike(Guid id)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var bike = await context.Bikes.Where(x => x.Id == id).Select(x => new BikeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Stock = x.Stock,
                    Kind = x.Kind,
                    ImageSrc = x.ImageSrc
                }).FirstOrDefaultAsync();

                return OperationResult.CreateSuccess(bike);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<BikeViewModel>(ex.Message);
            }
        }

        public async Task<OperationResult> CreateBike(BikeViewModel vm)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var data = new Bike
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Price = vm.Price,
                    Stock = vm.Stock,
                    Kind = vm.Kind,
                    Type = vm.Type
                };

                context.Bikes.Add(data);

                data.ImageSrc = (await _productImageStorage.UpdateProductImage(vm, data)).Result;
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> UpdateBike(BikeViewModel vm)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var entity = await context.Bikes.FirstOrDefaultAsync(x => x.Id == vm.Id);
                entity.Name = vm.Name;
                entity.Price = vm.Price;
                entity.Kind = vm.Kind;
                entity.Stock = vm.Stock;
                entity.ImageSrc = (await _productImageStorage.UpdateProductImage(vm, entity)).Result;

                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> DeleteBike(Guid id)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var bike = await context.Bikes.FirstOrDefaultAsync(x => x.Id == id);

                context.Bikes.Remove(bike);
                await context.SaveChangesAsync();

                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> DeleteBikeImage(Guid bikeId)
        {
            return await _productImageStorage.DeleteProductImage(bikeId);
        }
    }
}
