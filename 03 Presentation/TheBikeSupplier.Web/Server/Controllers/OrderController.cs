using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.DataServices.Implementations;
using TheBikeSupplier.Models.Client;

namespace TheBikeSupplier.Web.Server.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDataService _orderDataService;

        public OrderController(IOrderDataService orderDataService)
        {
            _orderDataService = orderDataService;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult> CreateOrder([FromBody] List<OrderAddressViewModel> orderAddresses, Guid basketId)
        {
            var result = await _orderDataService.CreateOrder(orderAddresses, basketId);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }
    }
}
