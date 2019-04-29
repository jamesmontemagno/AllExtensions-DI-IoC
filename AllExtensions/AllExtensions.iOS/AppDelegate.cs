using System;
using System.Collections.Generic;
using System.Linq;
using AllExtensions.Helpers;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UIKit;

namespace AllExtensions.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(Startup.Init(ConfigureServices));

            return base.FinishedLaunching(app, options);
        }

        void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddSingleton<INativeCalls, NativeCalls>();
        }
    }

    public class NativeCalls : INativeCalls
    {
        public void OpenToast(string text)
        {
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var okAlert = UIAlertController.Create(string.Empty, text, UIAlertControllerStyle.Alert);
            okAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            vc.PresentViewController(okAlert, true, null);
        }
    }
}
