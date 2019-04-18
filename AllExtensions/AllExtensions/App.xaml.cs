using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using Xamarin.Essentials;

namespace AllExtensions
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public App()
        {
            InitializeComponent();

            string systemDir = FileSystem.CacheDirectory;
            ExtractSaveResource("AllExtensions.appsettings.json", systemDir);
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

            ServiceProvider = host.Services;

            MainPage = ServiceProvider.GetService<MainPage>();
        }

        void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            if(ctx.HostingEnvironment.IsDevelopment())
            {
                var world = ctx.Configuration["Hello"];
            }

            services.AddHttpClient();
            services.AddSingleton<IMainViewModel, MainViewModel>();
            services.AddSingleton<MainPage>();
        }

        public static void ExtractSaveResource(string filename, string location)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream != null)
                {
                    var full = Path.Combine(location, filename);

                    using (var stream = File.Create(full))
                    {
                        resFilestream.CopyTo(stream);
                    }

                }
            }
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
