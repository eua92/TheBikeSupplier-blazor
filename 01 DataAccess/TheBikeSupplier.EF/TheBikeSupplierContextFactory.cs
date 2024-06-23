using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TheBikeSupplier.EF
{
    //A factory for creating derived DbContext instances.
    public class TheBikeSupplierContextFactory : IDbContextFactory<TheBikeSupplierContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public TheBikeSupplierContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TheBikeSupplierContext CreateDbContext()
        {
            return new TheBikeSupplierContext(_serviceProvider.GetRequiredService<DbContextOptions<TheBikeSupplierContext>>());
        }
    }
}
