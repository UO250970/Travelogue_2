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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzI4NTQ5QDMxMzgyZTMzMmUzMGlTMHlEeHdwSGkyU3ZKb2dCcjArT0dtUXlUcmJkYzJnR2VPQzBsZm1kL0U9");
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
