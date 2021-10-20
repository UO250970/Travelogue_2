using System.Globalization;
using Travelogue_2.Resources.Localization;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;
using System;
using System.Diagnostics;

namespace Travelogue_2
{
    public partial class App : Application
    {
        public static string CurrentLanguage = "ES";
        public static LocalizedResources LocResources { get; private set; }

        public App()
        {
            try
            {
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDkzOTY2QDMxMzkyZTMyMmUzMGtocktKSU5vaHZ3ZXI3UVN4UXE4eU9FNWdoOHBDQ2ZtaDdRYU0rTElhUEE9");
                InitializeComponent();

                MainPage = new AppShell();

                DependencyService.Register<INotifications>();

                AppResources.Culture = new CultureInfo(CurrentLanguage);
                LocResources = new LocalizedResources(typeof(AppResources), CurrentLanguage);

                // Alerter de la aplicación
                Alerter.SetPage(MainPage);
                DataBaseUtil.Start();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
