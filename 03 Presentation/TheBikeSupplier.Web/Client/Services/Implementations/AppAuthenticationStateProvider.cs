using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Models.Client.Authorize;
using TheBikeSupplier.Web.Client.Services.Interfaces;

namespace TheBikeSupplier.Web.Client.Services.Implementations
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private UserInfo _userInCache;
        private readonly IAuthorizeApiService _authorizeApiService;

        public AppAuthenticationStateProvider(IAuthorizeApiService authorizeApiService)
        {
            _authorizeApiService = authorizeApiService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var currentUser = await GetUserInfo();
                if (currentUser.IsAuthenticated)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, currentUser.UserName) }.Concat(currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task<OperationResult> Register(RegisterParameters registerParameters)
        {
            var result = await _authorizeApiService.Register(registerParameters);
            if (result.Failed)
                return result;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return OperationResult.CreateSuccess();
        }

        public async Task<OperationResult> Login(LoginParameters loginParameters)
        {
            var result = await _authorizeApiService.Login(loginParameters);
            if (result.Failed)
                return result;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return OperationResult.CreateSuccess();
        }

        public async Task Logout()
        {
            await _authorizeApiService.Logout();
            _userInCache = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<UserInfo> GetUserInfo()
        {
            if (_userInCache?.UserName != null && _userInCache.IsAuthenticated) return _userInCache;
            _userInCache = await _authorizeApiService.GetUserInfo();
            return _userInCache;
        }
    }
}

