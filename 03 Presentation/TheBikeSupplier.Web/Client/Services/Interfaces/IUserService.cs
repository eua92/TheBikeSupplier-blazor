using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBikeSupplier.Models.Client.Dtos;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.Web.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task GetCurrentUser();
        AppUserViewModel CurrentUser { get; set; }
        Task<UserDataDto> CreateUserDataDto(ClaimsPrincipal currentUser);
    }
}
