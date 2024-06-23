using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.EF;
using TheBikeSupplier.Models;
using TheBikeSupplier.Models.Client;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.DataServices.Implementations
{
    public class BasketDataService : IBasketDataService
    {
        private readonly IDbContextFactory<TheBikeSupplierContext> _dbContextFactory;

        public BasketDataService(IDbContextFactory<TheBikeSupplierContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult<BasketViewModel>> GetBasketItems(Guid basketId)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var data = await context.Baskets.Where(x => x.Id == basketId).Select(x => new BasketViewModel
                {
                    Id = x.Id,
                    Status = x.Status,
                    BasketItems = x.BasketItems.Select(p => new BasketItemViewModel
                    {
                        Id = p.Id,
                        BasketId = p.BasketId,
                        ProductId = p.ProductId,
                        Name = p.Product.Name,
                        Price = p.Product.Price,
                        ImageSrc = p.Product.ImageSrc,
                        Quantity = p.Quantity
                    }).ToList()
                }).FirstOrDefaultAsync();

                if (data == null)
                    return OperationResult.CreateSuccess(new BasketViewModel());

                return OperationResult.CreateSuccess(data);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<BasketViewModel>(ex.Message);
            }
        }

        public async Task<OperationResult<BasketInfo>> GetBasketInfo(Guid ownerId, bool isAuthenticated)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var query = context.Baskets.Include(x => x.BasketItems).Where(x => x.Status == BasketStatus.Active
                    || x.Status == BasketStatus.Pending || x.Status == BasketStatus.Abandoned);

                if (isAuthenticated)
                    query = query.Where(x => x.UserId == ownerId);
                else
                    query = query.Where(x => x.VisitorId == ownerId);

                var basketInfo = await query.Select(x => new BasketInfo 
                { 
                    Id = x.Id,
                    Count = x.BasketItems.Sum(x => x.Quantity)
                }).FirstOrDefaultAsync();
                
                return OperationResult.CreateSuccess(basketInfo ?? new BasketInfo());
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<BasketInfo>(ex.Message);
            }
        }

        public async Task<OperationResult<BasketInfo>> CreateBasket(Guid bikeId, Guid ownerId, bool isAuthenticated)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                if (await IsOutOfStock(context, bikeId))
                    return OperationResult.CreateFailure<BasketInfo>("Item is not in stock");

                var basket = new Basket
                {
                    UserId = isAuthenticated ? ownerId : null,
                    VisitorId = isAuthenticated ? null : ownerId,
                    Status = BasketStatus.Active,
                    CreatedDate = DateTime.Now,
                    BasketItems = new List<BasketItem>()
                    {
                        new BasketItem
                        {
                            ProductId = bikeId,
                            Quantity = 1
                        }
                    }
                };
                context.Baskets.Add(basket);
                var basketInfo = new BasketInfo
                {
                    Id = basket.Id,
                    Count = CalculateTotalBasketCount(basket)
                };

                await UpdateStock(context, bikeId, 1);
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess(basketInfo);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<BasketInfo>(ex.Message);
            }
        }

        public async Task<OperationResult<BasketInfo>> UpdateBasket(Guid bikeId, Guid basketId)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                if (await IsOutOfStock(context, bikeId))
                    return OperationResult.CreateFailure<BasketInfo>("Item is not in stock");

                var entity = await context.Baskets.Include(x => x.BasketItems).Where(x => x.Id == basketId).FirstOrDefaultAsync();
                if (entity == null)
                    return OperationResult.CreateFailure<BasketInfo>("Basket not found");

                var basketItem = entity.BasketItems.Where(x => x.ProductId == bikeId).FirstOrDefault();
                if (basketItem == null)
                {
                    entity.BasketItems.Add(new BasketItem
                    {
                        ProductId = bikeId,
                        Quantity = 1
                    });
                }
                else
                {
                    basketItem.Quantity++;
                }                   
                entity.UpdatedDate = DateTime.Now;
                var basketInfo = new BasketInfo
                {
                    Id = entity.Id,
                    Count = CalculateTotalBasketCount(entity)
                };

                await UpdateStock(context, bikeId, 1);
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess(basketInfo);
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure<BasketInfo>(ex.Message);
            }
        }

        public async Task<OperationResult> AddBasketItems(Guid basketId, Guid userId)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var query = context.Baskets.Include(x => x.BasketItems);

                var visitorBasket = await query.Where(x => x.Id == basketId).FirstOrDefaultAsync();
                var userBasket = await query.Where(x => x.UserId == userId && (x.Status == BasketStatus.Active
                       || x.Status == BasketStatus.Pending || x.Status == BasketStatus.Abandoned)).FirstOrDefaultAsync();

                if (userBasket != null)
                {
                    foreach (var item in visitorBasket.BasketItems)
                    {
                        var basketItem = userBasket.BasketItems.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                        if (basketItem == null)
                        {
                            userBasket.BasketItems.Add(new BasketItem
                            {
                                ProductId = item.ProductId,
                                Quantity = item.Quantity
                            });
                        }
                        else
                        {
                            basketItem.Quantity += item.Quantity;
                        }
                    }
                    userBasket.UpdatedDate = DateTime.Now;
                    context.Baskets.Remove(visitorBasket);
                }
                else
                {
                    visitorBasket.UserId = userId;
                    visitorBasket.VisitorId = null;
                }

                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> RemoveBasketItem(Guid basketId, Guid basketItemId)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var basket = await context.Baskets.Include(x => x.BasketItems).FirstOrDefaultAsync(x => x.Id == basketId);
                var basketItem = basket.BasketItems.FirstOrDefault(x => x.Id == basketItemId);

                basket.BasketItems.Remove(basketItem);
                if (basket.BasketItems.Count() == 0)
                    context.Baskets.Remove(basket);

                await UpdateStock(context, basketItem.ProductId, -1);
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> UpdateBasketItemQuantity(Guid itemId, int newQuantity)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var basketItem = await context.BasketItems.Include(x => x.Basket).FirstOrDefaultAsync(x => x.Id == itemId);

                var tempQuantity = newQuantity - basketItem.Quantity;
                basketItem.Quantity = newQuantity;
                basketItem.Basket.UpdatedDate = DateTime.Now;

                await UpdateStock(context, basketItem.ProductId, tempQuantity);
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> UpdateBasketStatus(Guid basketId, BasketStatus newStatus)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var basket = await context.Baskets.FirstOrDefaultAsync(x => x.Id == basketId);
                basket.Status = newStatus;
                basket.UpdatedDate = DateTime.Now;
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> SetAbandonedBasketStatus(DateTime abandonmentDate)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();         
                var abandonedBaskets = await context.Baskets.Where(x => x.Status == BasketStatus.Active && (x.UpdatedDate < abandonmentDate || x.UpdatedDate == null && x.CreatedDate < abandonmentDate)).ToListAsync();
                foreach (var basket in abandonedBaskets)
                {
                    basket.Status = BasketStatus.Abandoned;
                }
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult> SetExpiredBasketStatus(DateTime expirationDate)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var expiredBaskets = await context.Baskets.Include(x => x.BasketItems).Where(x => x.Status == BasketStatus.Abandoned && (x.UpdatedDate < expirationDate || x.UpdatedDate == null && x.CreatedDate < expirationDate)).ToListAsync();
                foreach (var basket in expiredBaskets)
                {
                    basket.Status = BasketStatus.Expired;
                    foreach (var basketItem in basket.BasketItems)
                    {
                        await UpdateStock(context, basketItem.ProductId, basketItem.Quantity * (-1));
                    }
                }
                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }

        private async Task<bool> IsOutOfStock(TheBikeSupplierContext context, Guid productId)
        {
            var bike = await context.Bikes.FirstOrDefaultAsync(x => x.Id == productId);
            return bike.Stock < 1;
        }

        private async Task UpdateStock(TheBikeSupplierContext context, Guid productId, int newQuantity)
        {
            var bike = await context.Bikes.FirstOrDefaultAsync(x => x.Id == productId);
            bike.Stock -= newQuantity;
            bike.ItemsInBasket += newQuantity;
        }

        private int CalculateTotalBasketCount(Basket basket)
        {
            return basket?.BasketItems?.Sum(x => x.Quantity) ?? 0;
        }
    }
}
