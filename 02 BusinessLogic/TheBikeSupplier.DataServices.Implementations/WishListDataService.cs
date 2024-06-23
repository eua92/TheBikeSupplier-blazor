using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.EF;
using TheBikeSupplier.Models;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.DataServices.Implementations
{
    public class WishListDataService : IWishListDataService
    {
        private readonly IDbContextFactory<TheBikeSupplierContext> _dbContextFactory;

        public WishListDataService(IDbContextFactory<TheBikeSupplierContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult<Guid>> GetWishListId(Guid ownerId, bool isAuthenticated)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                IQueryable<WishList> query;

                if (isAuthenticated)
                    query = context.WishLists.Where(x => x.UserId == ownerId);
                else
                    query = context.WishLists.Where(x => x.VisitorId == ownerId);

                var wishListId = await query.Select(x => x.Id).FirstOrDefaultAsync();
                return OperationResult.CreateSuccess(wishListId);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<Guid>(ex.Message);
            }
        }

        public async Task<OperationResult<WishListViewModel>> GetWishList(Guid wishListId)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var data = await context.WishLists.Where(x => x.Id == wishListId).Select(x => new WishListViewModel
                {
                    Id = x.Id,
                    WishListItems = x.WishListItems.Select(p => new WishListItemViewModel
                    {
                        Id = p.Id,
                        WishListId = p.WishListId,
                        ProductId = p.ProductId,
                        Name = p.Product.Name,
                        Price = p.Product.Price,
                        ImageSrc = p.Product.ImageSrc
                    }).ToList()
                }).FirstOrDefaultAsync();

                if (data == null)
                    return OperationResult.CreateSuccess(new WishListViewModel());

                return OperationResult.CreateSuccess(data);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<WishListViewModel>(ex.Message);
            }
        }

        public async Task<OperationResult<Guid>> CreateWishList(Guid bikeId, Guid ownerId, bool isAuthenticated)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var wishList = new WishList
                {
                    UserId = isAuthenticated ? ownerId : null,
                    VisitorId = isAuthenticated ? null : ownerId,
                    WishListItems = new List<WishListItem>()
                    {
                        new WishListItem
                        {
                            ProductId = bikeId,
                        }
                    }
                };
                context.WishLists.Add(wishList);
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess(wishList.Id);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<Guid>(ex.Message);
            }
        }

        public async Task<OperationResult<Guid>> UpdateWishList(Guid bikeId, Guid wishListId)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();

                var entity = await context.WishLists.Include(x => x.WishListItems).Where(x => x.Id == wishListId).FirstOrDefaultAsync();
                if (entity == null)
                    return OperationResult.CreateFailure<Guid>("Wish list not found");

                if (!entity.WishListItems.Any(x => x.ProductId == bikeId))
                {
                    entity.WishListItems.Add(new WishListItem
                    {
                        ProductId = bikeId
                    });
                }
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess(entity.Id);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<Guid>(ex.Message);
            }
        }
    }
}