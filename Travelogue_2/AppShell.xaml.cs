using Travelogue_2.Main.Views;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.Library;
using Travelogue_2.Main.Views.Library.Create;
using Travelogue_2.Main.Views.Media;
using Travelogue_2.Main.Views.Modelation;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Travelogue_2.Main.Views.PopUps;
using Travelogue_2.Main.Views.Settings;
using Xamarin.Forms;

namespace Travelogue_2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(JourneyView), typeof(JourneyView));
            Routing.RegisterRoute(nameof(JourneySettingsView), typeof(JourneySettingsView));
            Routing.RegisterRoute(nameof(CreateEventView), typeof(CreateEventView));
            Routing.RegisterRoute(nameof(CreateEntryView), typeof(CreateEntryView));

            Routing.RegisterRoute(nameof(CreatedJourneysView), typeof(CreatedJourneysView));
            Routing.RegisterRoute(nameof(ClosedJourneysView), typeof(ClosedJourneysView));
            Routing.RegisterRoute(nameof(CreateJourneyView), typeof(CreateJourneyView));

            Routing.RegisterRoute(nameof(StartModelationView), typeof(StartModelationView));
            Routing.RegisterRoute(nameof(ContinueModelationView), typeof(ContinueModelationView));
            Routing.RegisterRoute(nameof(EndedModelationView), typeof(EndedModelationView));
            Routing.RegisterRoute(nameof(JournalModelationView), typeof(JournalModelationView));

            Routing.RegisterRoute(nameof(SettingsLanguageView), typeof(SettingsLanguageView));
            Routing.RegisterRoute(nameof(SettingsCardHolderView), typeof(SettingsCardHolderView));
            Routing.RegisterRoute(nameof(SettingsDestiniesView), typeof(SettingsDestiniesView));
            Routing.RegisterRoute(nameof(SettingsStyleView), typeof(SettingsStyleView));

            Routing.RegisterRoute(nameof(AddEventPopUp), typeof(AddEventPopUp));
            Routing.RegisterRoute(nameof(EditOrDeleteEventPopUp), typeof(EditOrDeleteEventPopUp));
            Routing.RegisterRoute(nameof(AddEntryPopUp), typeof(AddEntryPopUp));
            Routing.RegisterRoute(nameof(EditOrDeleteEntryPopUp), typeof(EditOrDeleteEntryPopUp));
            Routing.RegisterRoute(nameof(AddToEntryPopUp), typeof(AddToEntryPopUp));
            Routing.RegisterRoute(nameof(EditOrDeleteFromEntryPopUp), typeof(EditOrDeleteFromEntryPopUp));

            Routing.RegisterRoute(nameof(AddDestinyPopUp), typeof(AddDestinyPopUp));
            Routing.RegisterRoute(nameof(EditOrDeleteDestinyPopUp), typeof(EditOrDeleteDestinyPopUp));
            Routing.RegisterRoute(nameof(AddCardPopUp), typeof(AddCardPopUp));
            Routing.RegisterRoute(nameof(EditOrDeleteCardPopUp), typeof(EditOrDeleteCardPopUp));

            Routing.RegisterRoute(nameof(ImageView), typeof(ImageView));
            Routing.RegisterRoute(nameof(BackgroundSelectorView), typeof(BackgroundSelectorView));
            Routing.RegisterRoute(nameof(PageModelationView), typeof(PageModelationView));
        }

    }
}
