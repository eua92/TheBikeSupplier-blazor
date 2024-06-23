using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.Service.Services.Interfaces;

namespace TheBikeSupplier.Service.Services.Implementations
{
    public class WorkerService : IWorkerService
    {
        private readonly IBasketDataService _basketDataService;

        public WorkerService(IBasketDataService basketDataService)
        {
            _basketDataService = basketDataService;
        }

        public async Task SetExpiredBasketStatus()
        {
            var abandonmentDate = DateTime.Now.AddMinutes(-30);
            await _basketDataService.SetExpiredBasketStatus(abandonmentDate);
        }

        public async Task SetAbandonedBasketStatus()
        {
            var expirationDate = DateTime.Now.AddHours(-2);
            await _basketDataService.SetAbandonedBasketStatus(expirationDate);
        }
    }
}
