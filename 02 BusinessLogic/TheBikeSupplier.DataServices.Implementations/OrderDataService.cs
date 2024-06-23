using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.EF;
using TheBikeSupplier.Models;
using TheBikeSupplier.Models.Client;

namespace TheBikeSupplier.DataServices.Implementations
{
    
    public class OrderDataService : IOrderDataService
    {
        private readonly IDbContextFactory<TheBikeSupplierContext> _dbContextFactory;
        public OrderDataService(IDbContextFactory<TheBikeSupplierContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<OperationResult> CreateOrder(List<OrderAddressViewModel> orderAddresses, Guid basketId)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var basket = await context.Baskets.Include(x => x.BasketItems).ThenInclude(p => p.Product).FirstOrDefaultAsync(x => x.Id == basketId);
                var data = new Order
                {
                    Status = OrderStatus.InProcess,
                    TotalPrice = basket.BasketItems.Sum(x => x.Product.Price * x.Quantity),
                    OrderDetails = basket.BasketItems.Select(x => new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity
                    }).ToList(),
                    OrderAddresses = orderAddresses.Select(x => new OrderAddress
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
                        Address = x.Address,
                        PostCode = x.PostCode,
                        City = x.City,
                        Country = x.Country,
                        Type = x.Type
                    }).ToList()
                };
                context.Orders.Add(data);
                basket.Status = BasketStatus.Completed;

                foreach (var item in basket.BasketItems)
                {
                    var bike = await context.Bikes.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                    bike.ItemsInBasket -= item.Quantity;
                }

                await context.SaveChangesAsync();
                return OperationResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure(ex.Message);
            }
        }
    }   
}
