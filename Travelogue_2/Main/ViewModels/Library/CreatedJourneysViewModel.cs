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

			JourneyTapped = new Command<Item>(OnJourneySelected);

			ExecuteLoadJourneysCommand();
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

				JourneyCard temp3 = new JourneyCard();
				temp3.Name = "Prueba3";
				temp3.Image = ImageSource.FromResource(CommonVariables.GenericImage);

				JourneyCard temp4 = new JourneyCard();
				temp4.Name = "Prueba3";
				temp4.Image = ImageSource.FromResource(CommonVariables.GenericImage);

				JourneyCard temp5 = new JourneyCard();
				temp5.Name = "Prueba3";
				temp5.Image = ImageSource.FromResource(CommonVariables.GenericImage);

				JourneyCard temp6 = new JourneyCard();
				temp6.Name = "Prueba3";
				temp6.Image = ImageSource.FromResource(CommonVariables.GenericImage);

				JourneyCard temp7 = new JourneyCard();
				temp7.Name = "Prueba3";
				temp7.Image = ImageSource.FromResource(CommonVariables.GenericImage);

				JourneyCard temp8 = new JourneyCard();
				temp8.Name = "Prueba3";
				temp8.Image = ImageSource.FromResource(CommonVariables.GenericImage);

				JourneysCreated.Add(temp1);
				JourneysCreated.Add(temp2);
				JourneysCreated.Add(temp3);
				JourneysCreated.Add(temp4);
				JourneysCreated.Add(temp5);
				JourneysCreated.Add(temp6);
				JourneysCreated.Add(temp7);
				JourneysCreated.Add(temp8);
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
			=> await Shell.Current.GoToAsync(nameof(CreateJourneyView));

		async void OnJourneySelected(Item journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyDetailView)}?{nameof(ItemDetailViewModel.ItemId)}={journey.Id}");
		}

	}
}
