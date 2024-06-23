using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.EF;
using TheBikeSupplier.Models;
using TheBikeSupplier.Models.Client;
using TheBikeSupplier.Models.Client.Authorize;
using TheBikeSupplier.Resources;

namespace TheBikeSupplier.Web.Server.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserDataService _userDataService;

        public AuthorizeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IUserDataService userDataService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userDataService = userDataService;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginParameters loginParameters)
        {
            var user = await _userManager.FindByEmailAsync(loginParameters.UserName);
            if (user == null) return Problem(StringResources.IncorrectEmailAddress);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginParameters.Password, false);
            if (!signInResult.Succeeded) return Problem(StringResources.IncorrectPassword);

            await _signInManager.SignInAsync(user, loginParameters.RememberMe);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterParameters registerParameters)
        {
            var user = new ApplicationUser()
            {
                UserName = registerParameters.UserName,
                Email = registerParameters.UserName,
                FirstName = registerParameters.FirstName,
                LastName = registerParameters.LastName,
                PhoneNumber = registerParameters.PhoneNumber,
                DateOfBirth = registerParameters.DateOfBirth.GetValueOrDefault()
            };

            var result = await _userManager.CreateAsync(user, registerParameters.Password);
            if (!result.Succeeded)
                return Problem(result.Errors.FirstOrDefault()?.Description);

            var addToRoleResult = await _userManager.AddToRoleAsync(user, Roles.User);
            if (!addToRoleResult.Succeeded)
                return Problem(addToRoleResult.Errors.FirstOrDefault()?.Description);

            return await Login(new LoginParameters
            {
                UserName = registerParameters.UserName,
                Password = registerParameters.Password
            });
        }

        [HttpGet]
        public async Task<ActionResult<UserInfo>> GetUserInfo()
        {
            var userInfo = new UserInfo
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value),
                UserId = await _userDataService.GetUserIdByUserName(User.Identity.Name)
            };
            return Ok(userInfo);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
