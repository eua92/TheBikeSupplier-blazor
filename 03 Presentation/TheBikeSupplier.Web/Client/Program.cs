using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sotsera.Blazor.Toaster.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;
using TheBikeSupplier.Common.Extensions;
using TheBikeSupplier.Common.Interfaces;
using TheBikeSupplier.Web.Client.Services.Implementations;
using TheBikeSupplier.Web.Client.Services.Interfaces;

namespace TheBikeSupplier.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AppAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AppAuthenticationStateProvider>());
            builder.Services.AddSweetAlert2(options =>
            {
                options.Theme = SweetAlertTheme.Default;
            });
            builder.Services.AddToaster(config =>
            {
                config.VisibleStateDuration = 10;
                config.PositionClass = Defaults.Classes.Position.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = false;
                config.MaxDisplayedToasts = 5;
            });
            builder.Services.AddTransient<IAuthorizeApiService, AuthorizeApiService>();
            builder.Services.AddTransient<IDialogService, DialogService>();
            builder.Services.AddTransient<IToasterService, ToasterService>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<IWishListService, WishListService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<IEnumTypeService, EnumTypeService>();
            builder.Services.AddBlazorDialog();
            builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);
            builder.Services.AddBlazoredLocalStorage();

            await builder.Build().RunAsync();
        }
    }
}
