using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;

namespace TheBikeSupplier.Models
{
    public class Tour : Product
    {
        public TourDurationType DurationType { get; set; }
        public int Duration { get; set; }
        public TourLevel Level { get; set; }
        public int MaximumPersons{ get; set; }
        public double Distance { get; set; }
        public TourDistanceUnit DistanceUnit { get; set; }
    }
}
