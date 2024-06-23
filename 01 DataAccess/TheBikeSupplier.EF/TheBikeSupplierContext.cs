using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Models;

namespace TheBikeSupplier.EF
{
    public class TheBikeSupplierContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IDataProtectionKeyContext
    {
        public TheBikeSupplierContext(DbContextOptions<TheBikeSupplierContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Order> Orders  { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
}
}
