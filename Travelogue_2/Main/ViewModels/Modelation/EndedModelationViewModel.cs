using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
    public class EndedModelationViewModel : DataBaseViewModel
    {

        public Command<Item> JourneyTapped { get; }
        public ObservableCollection<JourneyModel> ClosedJourals { get; set; }

        public EndedModelationViewModel()
        {
            ClosedJourals = new ObservableCollection<JourneyModel>();

            JourneyTapped = new Command<Item>(OnJourneySelected);

            ExecuteLoadDataCommand();
        }

        public override void LoadData()
        {
        }

        void OnJourneySelected(Item journey)
        {
            if (journey == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(JourneyOngoingView)}?{nameof(ItemDetailViewModel.ItemId)}={journey.Id}");
        }
    }
}
