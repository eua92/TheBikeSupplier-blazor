using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.Models.Client.Interfaces;

namespace TheBikeSupplier.Models.Client
{
    public class TourViewModel : IProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageSrc { get; set; }
        public byte[] ImageFileContent { get; set; }
        public TourDurationType DurationType { get; set; }
        public int Duration { get; set; }
        public TourLevel Level { get; set; }
        public int MaximumPersons { get; set; }
        public double Distance { get; set; }
        public TourDistanceUnit DistanceUnit { get; set; }
    }

    public class TourDistanceViewModel
    {
        public double Distance { get; set; }
        public string Unit { get; set; }
    }
}
