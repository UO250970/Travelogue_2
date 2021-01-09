using System.Globalization;
using Travelogue_2.Resources.Localization;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2
{
    public partial class App : Application
    {
        public static string CurrentLanguage = "";
        public static LocalizedResources LocResources { get; private set; }


        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzgwNTEzQDMxMzgyZTM0MmUzMGd5ZzdMS2RINmM2NkNVQ0s0Y0szNElMS1Q0cmxoWW1nUmJIUlBiNDNBcE09");
            InitializeComponent();

            AppResources.Culture = new CultureInfo(string.Empty);
            LocResources = new LocalizedResources(typeof(AppResources), CurrentLanguage);

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

            // Alerter de la aplicación
            Alerter.SetPage(MainPage);
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
