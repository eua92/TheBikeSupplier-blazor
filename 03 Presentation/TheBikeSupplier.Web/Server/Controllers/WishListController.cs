using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.Models.Client.Dtos;

namespace TheBikeSupplier.Web.Server.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListDataService _wishListDataService;

        public WishListController(IWishListDataService wishListDataService)
        {
            _wishListDataService = wishListDataService;
        }

        [HttpPost]
        public async Task<ActionResult> GetWishListId([FromBody] UserDataDto request)
        {
            var result = await _wishListDataService.GetWishListId(request.OwnerId, request.IsAuthenticated);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpGet("{wishListId}")]
        public async Task<ActionResult> GetWishList(Guid wishListId)
        {
            var result = await _wishListDataService.GetWishList(wishListId);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpPost("{bikeId}")]
        public async Task<ActionResult> CreateWishList([FromBody] UserDataDto request, Guid bikeId)
        {
            var result = await _wishListDataService.CreateWishList(bikeId, request.OwnerId, request.IsAuthenticated);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpPost("{bikeId}")]
        public async Task<ActionResult> UpdateWishList([FromBody] Guid wishListId, Guid bikeId)
        {
            var result = await _wishListDataService.UpdateWishList(bikeId, wishListId);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }
    }
}
