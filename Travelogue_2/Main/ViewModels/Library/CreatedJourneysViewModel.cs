using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.Library.Create;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
	public class CreatedJourneysViewModel : BaseViewModel
    {

		public Command CreateJourneyCommand { get; }


		public Command<Item> JourneyTapped { get; }
		public ObservableCollection<JourneyCard> JourneysCreated { get; set; }

		

		public CreatedJourneysViewModel()
		{

			CreateJourneyCommand = new Command(() => CreateJourneyC());

			JourneysCreated = new ObservableCollection<JourneyCard>();
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
				temp2.Name = "Prueba3";
				temp2.Image = ImageSource.FromResource(CommonVariables.GenericImage);

				JourneysCreated.Add(temp1);
				JourneysCreated.Add(temp2);
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


		async internal void CreateJourneyC()
			=> await Shell.Current.GoToAsync(nameof(CreateJourneyOneView));

		async void OnJourneySelected(Item journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyDetailView)}?{nameof(ItemDetailViewModel.ItemId)}={journey.Id}");
		}

	}
}
