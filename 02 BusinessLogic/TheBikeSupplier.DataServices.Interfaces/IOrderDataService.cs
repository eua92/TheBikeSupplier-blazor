using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Models.Client;

namespace TheBikeSupplier.DataServices.Implementations
{
    public interface IOrderDataService
    {
        Task<OperationResult> CreateOrder(List<OrderAddressViewModel> orderAddresses, Guid basketId);
    }
}
