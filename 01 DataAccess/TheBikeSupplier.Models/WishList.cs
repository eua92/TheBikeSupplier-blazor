using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBikeSupplier.Models
{
    public class WishList : EntityBase
    {
        public Guid? UserId { get; set; }
        public Guid? VisitorId { get; set; }
        public List<WishListItem> WishListItems { get; set; }
    }

    public class WishListItem : EntityBase
    {
        public Guid WishListId { get; set; }
        public WishList WishList { get; set; }
        public Guid ProductId { get; set; }
        public Bike Product { get; set; }
    }
}
