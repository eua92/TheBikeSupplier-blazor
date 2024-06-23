using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Service.Services.Interfaces;
using TheBikeSupplier.Service.Services.Implementations;
using TheBikeSupplier.DataServices.Interfaces;
using TheBikeSupplier.DataServices.Implementations;
using TheBikeSupplier.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheBikeSupplier.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.AddDbContext<TheBikeSupplierContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("TheBikeSupplierContext")));
                    services.AddScoped<IDbContextFactory<TheBikeSupplierContext>, TheBikeSupplierContextFactory>();

                    services.AddScoped<IWorkerService, WorkerService>();
                    services.AddScoped<IBasketDataService, BasketDataService>();
                });
    }
}
