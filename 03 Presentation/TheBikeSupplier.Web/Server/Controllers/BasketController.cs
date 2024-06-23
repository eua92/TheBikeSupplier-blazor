using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.Models;
using TheBikeSupplier.Models.Client;
using TheBikeSupplier.Models.Client.Dtos;

namespace TheBikeSupplier.Web.Server.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketDataService _basketDataService;

        public BasketController(IBasketDataService basketDataService)
        {
            _basketDataService = basketDataService;
        }

        [HttpGet("{basketId}")]
        public async Task<ActionResult> GetBasketItems(Guid basketId)
        {
            var result = await _basketDataService.GetBasketItems(basketId);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpPost]
        public async Task<ActionResult> GetBasketInfo([FromBody] UserDataDto request)
        {
            var result = await _basketDataService.GetBasketInfo(request.OwnerId, request.IsAuthenticated);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpPost("{bikeId}")]
        public async Task<ActionResult> CreateBasket([FromBody] UserDataDto request, Guid bikeId)
        {
            var result = await _basketDataService.CreateBasket(bikeId, request.OwnerId, request.IsAuthenticated);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpPost("{bikeId}")]
        public async Task<ActionResult> UpdateBasket([FromBody] Guid basketId, Guid bikeId)
        {
            var result = await _basketDataService.UpdateBasket(bikeId, basketId);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult> AddBasketItems([FromBody] UserDataDto request, Guid basketId)
        {
            var result = await _basketDataService.AddBasketItems(basketId, request.OwnerId);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }

        [HttpDelete("{basketId}/{basketItemId}")]
        public async Task<ActionResult> RemoveBasketItem(Guid basketId, Guid basketItemId)
        {
            var result = await _basketDataService.RemoveBasketItem(basketId, basketItemId);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }

        [HttpPost("{basketItemId}")]
        public async Task<ActionResult> UpdateBasketItemQuantity([FromBody] int newQuantity, Guid basketItemId)
        {
            var result = await _basketDataService.UpdateBasketItemQuantity(basketItemId, newQuantity);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult> UpdateBasketStatus([FromBody] BasketStatus newStatus, Guid basketId)
        {
            var result = await _basketDataService.UpdateBasketStatus(basketId, newStatus);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }
    }
}
