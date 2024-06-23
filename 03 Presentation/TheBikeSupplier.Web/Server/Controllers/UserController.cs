using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.Web.Server.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDataService _userDataService;

        public UserController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [HttpGet]
        public async Task<ActionResult<AppUserViewModel>> GetCurrentUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId?.Value, out Guid userIdGuid))
            {
                var user = await _userDataService.GetCurrentUser(userIdGuid);
                return Ok(user);
            }
            return Problem();           
        }
    }
}
