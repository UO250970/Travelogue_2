using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Views.Journey;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
	public class ContinueModelationViewModel : DataBaseViewModel
	{

		public Command<Item> JourneyTapped { get; }
		public ObservableCollection<JourneyCard> ContinueJournals { get; set; }

		public ContinueModelationViewModel()
		{
			ContinueJournals = new ObservableCollection<JourneyCard>();

			JourneyTapped = new Command<Item>(OnJourneySelected);

			ExecuteLoadDataCommand();
		}
		public override void LoadData()
		{
		}

		async void OnJourneySelected(Item journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyOngoingView)}?{nameof(ItemDetailViewModelNU.ItemId)}={journey.Id}");
		}

	}
}
