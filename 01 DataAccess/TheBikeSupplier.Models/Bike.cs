using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;

namespace TheBikeSupplier.Models
{
    public class Bike : Product
    {
        public BikeKind Kind { get; set; }
        public AcquisitionType Type { get; set; }
        public int Stock { get; set; }
        public int ItemsInBasket { get; set; }
    }
}
