using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.Models.Client;

namespace TheBikeSupplier.Web.Server.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourDataService _tourDataService;

        public TourController(ITourDataService tourDataService)
        {
            _tourDataService = tourDataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TourViewModel>>> GetTours()
        {
            var result = await _tourDataService.GetTours();
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourViewModel>> GetTour(Guid id)
        {
            var result = await _tourDataService.GetTour(id);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok(result.Result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TourViewModel vm)
        {
            var result = await _tourDataService.CreateTour(vm);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Update([FromBody] TourViewModel vm)
        {
            var result = await _tourDataService.UpdateTour(vm);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTour(Guid id)
        {
            var result = await _tourDataService.DeleteTour(id);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTourImage(Guid id)
        {
            var result = await _tourDataService.DeleteTourImage(id);
            if (result.Failed)
                return Problem(result.ErrorMessage);

            return Ok();
        }
    }
}
