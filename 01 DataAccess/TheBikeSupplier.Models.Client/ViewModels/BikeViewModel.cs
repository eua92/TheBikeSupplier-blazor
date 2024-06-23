using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.Models.Client.Interfaces;

namespace TheBikeSupplier.Models.Client
{
    public class BikeViewModel : IProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageSrc { get; set; }
        public byte[] ImageFileContent { get; set; }
        public BikeKind Kind { get; set; }
        public AcquisitionType Type { get; set; }
        public int Stock { get; set; }
        public bool InWishList { get; set; }
    }
}
