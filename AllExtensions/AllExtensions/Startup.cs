using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace AllExtensions
{
    public static class Startup
    {
        public static App Init()
        {
            var systemDir = FileSystem.CacheDirectory;
            Utils.ExtractSaveResource("AllExtensions.appsettings.json", systemDir);
            var fullConfig = Path.Combine(systemDir, "AllExtensions.appsettings.json");

            var host = new HostBuilder()
                            .ConfigureHostConfiguration(c =>
                            {
                                c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                                c.AddJsonFile(fullConfig);
                            })
                            .ConfigureServices((c, x) => ConfigureServices(c, x))
                            .ConfigureLogging(l => l.AddConsole(o =>
                            {
                                o.DisableColors = true;
                            }))
                            .Build();

            App.ServiceProvider = host.Services;

            return App.ServiceProvider.GetService<App>();
        }


        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            if (ctx.HostingEnvironment.IsDevelopment())
            {
                var world = ctx.Configuration["Hello"];
            }

            services.AddHttpClient();
            services.AddTransient<IMainViewModel, MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddSingleton<App>();
        }

    }
}
