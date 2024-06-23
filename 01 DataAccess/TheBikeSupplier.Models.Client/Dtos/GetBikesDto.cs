using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;

namespace TheBikeSupplier.Models.Client.Dtos
{
    public class GetBikesDto
    {
        public AcquisitionType Type { get; set; }
        public Guid? WishListId { get; set; }
    }
}
