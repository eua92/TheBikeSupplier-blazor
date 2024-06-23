using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Models.Client.Authorize;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.DataServices.Interfaces
{
    public interface IUserDataService
    {
        Task<Guid> GetUserIdByUserName(string userName);
        Task<AppUserViewModel> GetCurrentUser(Guid userId);
    }
}
