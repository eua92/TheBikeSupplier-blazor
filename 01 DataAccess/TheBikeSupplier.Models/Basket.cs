using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;

namespace TheBikeSupplier.Models
{
    public class Basket : EntityBase
    {
        public Guid? UserId { get; set; }
        public Guid? VisitorId { get; set; }
        public BasketStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }

    public class BasketItem : EntityBase
    {
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Bike Product { get; set; }
    }
}
