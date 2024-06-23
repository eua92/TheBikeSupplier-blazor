using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBikeSupplier.Service.Services.Interfaces
{
    public interface IWorkerService
    {
        Task SetAbandonedBasketStatus();
        Task SetExpiredBasketStatus();
    }
}
