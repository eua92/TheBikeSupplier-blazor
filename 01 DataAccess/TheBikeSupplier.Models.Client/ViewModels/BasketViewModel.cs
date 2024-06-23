using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;

namespace TheBikeSupplier.Models.Client
{
    public class BasketViewModel
    {
        public Guid Id { get; set; }
        public BasketStatus Status { get; set; }
        public decimal TotalPrice => CalculateTotalPrice(BasketItems);
        public int TotalItems => CalculateTotalItems(BasketItems);
        public List<BasketItemViewModel> BasketItems { get; set; } = new();
        public List<OrderAddressViewModel> OrderAddresses { get; set; } = new();

        public decimal CalculateTotalPrice(List<BasketItemViewModel> basketItems)
        {
            return basketItems.Sum(x => x.TotalPrice);
        }

        public int CalculateTotalItems(List<BasketItemViewModel> basketItems)
        {
            return basketItems.Sum(x => x.Quantity);
        }
    }

    public class BasketItemViewModel
    {
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;
        public string ImageSrc { get; set; } = "";
        public int Quantity { get; set; } = 1;
    }
}
