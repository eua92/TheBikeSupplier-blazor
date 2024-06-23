using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using TheBikeSupplier.Common.Extensions;
using TheBikeSupplier.Common.Interfaces;
using TheBikeSupplier.DataServices.Implementations;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.EF;
using TheBikeSupplier.Models;
using TheBikeSupplier.Models.Config;
using TheBikeSupplier.Web.Server.Data;

namespace TheBikeSupplier.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<TheBikeSupplierContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TheBikeSupplierContext")));

            services.AddScoped<IDbContextFactory<TheBikeSupplierContext>, TheBikeSupplierContextFactory>();
            services.AddTransient<IBikeDataService, BikeDataService>();
            services.AddTransient<ITourDataService, TourDataService>();
            services.AddTransient<IOrderDataService, OrderDataService>();
            services.AddTransient<IUserDataService, UserDataService>();
            services.AddTransient<IBasketDataService, BasketDataService>();
            services.AddTransient<IWishListDataService, WishListDataService>();
            services.AddTransient<IAzureStorageService, AzureStorageService>();
            services.AddTransient(typeof(IProductImageStorage<,>), typeof(ProductImageStorage<,>));

            services.AddSingleton(x => new BlobServiceClient(Configuration.GetSection("AzureStorageSettings").Get<AzureStorageSettings>().ConnectionString));

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<TheBikeSupplierContext>();
            services.AddDataProtection().PersistKeysToDbContext<TheBikeSupplierContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.SlidingExpiration = true;
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;

                // User settings
                options.User.RequireUniqueEmail = true;
            });           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    DataSeed.SeedDatabase(serviceScope.ServiceProvider).GetAwaiter().GetResult();
                }
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
