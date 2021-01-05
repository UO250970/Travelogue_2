using Travelogue_2.Main.Views;
using Travelogue_2.Main.Views.Library;
using Travelogue_2.Main.Views.Settings;
using Xamarin.Forms;

namespace Travelogue_2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            Routing.RegisterRoute(nameof(CreatedJourneysView), typeof(CreatedJourneysView));
            Routing.RegisterRoute(nameof(ClosedJourneysView), typeof(ClosedJourneysView));

            Routing.RegisterRoute(nameof(SettingsLanguageView), typeof(SettingsLanguageView));
        }

    }
}
