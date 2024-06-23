using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Models.Client.Authorize;

namespace TheBikeSupplier.Web.Client.Services.Interfaces
{
    public interface IAuthorizeApiService
    {
        Task<OperationResult> Register(RegisterParameters registerParameters);
        Task<OperationResult> Login(LoginParameters loginParameters);
        Task Logout();
        Task<UserInfo> GetUserInfo();
    }
}
