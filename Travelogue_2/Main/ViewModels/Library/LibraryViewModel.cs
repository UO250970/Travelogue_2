using System.Collections.ObjectModel;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.ViewModels.Journal;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.Library;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
    public class LibraryViewModel : DataBaseViewModel
	{
		public Command LoadJourneysCommand { get; }
		public Command<JourneyCard> JourneyTapped { get; }
		public Command CreatedJourneysViewCommand { get; }
        public Command ClosedJourneysViewCommand { get; }


		public ObservableCollection<JourneyCard> JourneysCreated { get; }
		public ObservableCollection<JourneyCard> JourneysClosed { get; }

		public LibraryViewModel()
		{
			LoadJourneysCommand = new Command(async () => await ExecuteLoadDataCommand());
			
			CreatedJourneysViewCommand = new Command(() => CreatedJourneysViewC());
			ClosedJourneysViewCommand = new Command(() => ClosedJourneysViewC());

			JourneysCreated = new ObservableCollection<JourneyCard>();
			JourneysClosed = new ObservableCollection<JourneyCard>();

			JourneyTapped = new Command<JourneyCard>(OnJourneySelected);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			JourneysCreated.Clear();
			JourneyCard temp1 = new JourneyCard();
			temp1.Name = "Prueba";
			temp1.Image = ImageSource.FromResource(CommonVariables.GenericImage);


			JourneyCard temp2 = new JourneyCard();
			temp2.Name = "Prueba2";
			temp2.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			JourneysCreated.Add(temp1);
			JourneysCreated.Add(temp2);

			JourneysClosed.Clear();
			JourneysClosed.Add(temp1);
			//var items = await DataStore.GetItemsAsync(true);
			//foreach (var item in items)
			//{
			//Journeys.Add(item);
			//}
		}

		async internal void CreatedJourneysViewC()
			=> await Shell.Current.GoToAsync(nameof(CreatedJourneysView));
		

		async internal void ClosedJourneysViewC()
			=> await Shell.Current.GoToAsync(nameof(ClosedJourneysView));
		

		async void OnJourneySelected(JourneyCard journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyTemplateViewModel.JourneyId)}={journey.Id}");
		}

	}
}
