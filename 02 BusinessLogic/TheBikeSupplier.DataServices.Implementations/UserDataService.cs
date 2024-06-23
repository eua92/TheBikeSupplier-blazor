using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.EF;
using TheBikeSupplier.Models.Client.Authorize;
using TheBikeSupplier.Models.Client.ViewModels;

namespace TheBikeSupplier.DataServices.Implementations
{
    public class UserDataService : IUserDataService
    {
        private readonly IDbContextFactory<TheBikeSupplierContext> _dbContextFactory;

        public UserDataService(IDbContextFactory<TheBikeSupplierContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Guid> GetUserIdByUserName(string userName)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var userId = await context.Users.AsNoTracking().Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefaultAsync();
            return userId;
        }

        public async Task<AppUserViewModel> GetCurrentUser(Guid userId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var user = await context.Users.AsNoTracking().Select(x => new AppUserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth
            }).FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }
    }
}
