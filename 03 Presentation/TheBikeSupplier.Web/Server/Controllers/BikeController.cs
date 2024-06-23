using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheBikeSupplier.Common.Enums;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.Models.Client;
using TheBikeSupplier.Models.Client.Dtos;

namespace TheBikeSupplier.Web.Server.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IBikeDataService _bikeDataService;

        public BikeController(IBikeDataService bikeDataService)
        {
            _bikeDataService = bikeDataService;
        }

        [HttpPost]
        public async Task<ActionResult<List<BikeViewModel>>> GetBikes(GetBikesDto request)
        {
            var result = await _bikeDataService.GetBikes(request.Type, this.User, request.WishListId);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BikeViewModel>> GetBike(Guid id)
        {
            var result = await _bikeDataService.GetBike(id);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BikeViewModel vm)
        {
            var result = await _bikeDataService.CreateBike(vm);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Update([FromBody] BikeViewModel vm)
        {
            var result = await _bikeDataService.UpdateBike(vm);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBike(Guid id)
        {
            var result = await _bikeDataService.DeleteBike(id);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBikeImage(Guid id)
        {
            var result = await _bikeDataService.DeleteBikeImage(id);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }
    }
}
