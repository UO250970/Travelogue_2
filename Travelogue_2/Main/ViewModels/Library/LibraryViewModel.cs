using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.Library;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
    public class LibraryViewModel : BaseViewModel
	{
		public Command LoadJourneysCommand { get; }
		public Command<Item> JourneyTapped { get; }
		public Command CreatedJourneysViewCommand { get; }
        public Command ClosedJourneysViewCommand { get; }


		public ObservableCollection<JourneyCard> JourneysCreated { get; }
		public ObservableCollection<JourneyCard> JourneysClosed { get; }

		public LibraryViewModel()
		{
			LoadJourneysCommand = new Command(async () => await ExecuteLoadJourneysCommand());
			
			CreatedJourneysViewCommand = new Command(() => CreatedJourneysViewC());
			ClosedJourneysViewCommand = new Command(() => ClosedJourneysViewC());

			JourneysCreated = new ObservableCollection<JourneyCard>();
			JourneysClosed = new ObservableCollection<JourneyCard>();
			ExecuteLoadJourneysCommand();

			JourneyTapped = new Command<Item>(OnJourneySelected);
		}

		async Task ExecuteLoadJourneysCommand()
		{
			IsBusy = true;

			try
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
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		public void OnAppearing()
			=> IsBusy = true;


		async internal void CreatedJourneysViewC()
			=> await Shell.Current.GoToAsync(nameof(CreatedJourneysView));
		

		async internal void ClosedJourneysViewC()
			=> await Shell.Current.GoToAsync(nameof(ClosedJourneysView));
		

		async void OnJourneySelected(Item journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyDetailView)}?{nameof(ItemDetailViewModel.ItemId)}={journey.Id}");
		}

	}
}
