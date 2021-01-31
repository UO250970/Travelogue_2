using Travelogue_2.Main.Views;
using Travelogue_2.Main.Views.Library;
using Travelogue_2.Main.Views.Library.Create;
using Travelogue_2.Main.Views.Modelation;
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
            Routing.RegisterRoute(nameof(CreateJourneyOneView), typeof(CreateJourneyOneView));

            Routing.RegisterRoute(nameof(StartModelationView), typeof(StartModelationView));
            Routing.RegisterRoute(nameof(ContinueModelationView), typeof(ContinueModelationView));
            Routing.RegisterRoute(nameof(EndedModelationView), typeof(EndedModelationView));

            Routing.RegisterRoute(nameof(SettingsLanguageView), typeof(SettingsLanguageView));
        }

    }
}
