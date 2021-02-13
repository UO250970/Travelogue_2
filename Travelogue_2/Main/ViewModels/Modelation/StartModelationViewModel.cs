using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
	public class StartModelationViewModel : BaseViewModel
	{

		public Command<JourneyCard> JourneyTapped { get; }
		public ObservableCollection<JourneyCard> StartJourneys { get; set; }

		public StartModelationViewModel()
		{
			StartJourneys = new ObservableCollection<JourneyCard>();

			JourneyTapped = new Command<JourneyCard>(OnJourneySelected);

			ExecuteLoadJourneysCommand();
		}

		async Task ExecuteLoadJourneysCommand()
		{
			IsBusy = true;

			try
			{
				StartJourneys.Clear();
				JourneyCard temp1 = new JourneyCard();
				temp1.Name = "Prueba";
				temp1.Image = ImageSource.FromResource(CommonVariables.GenericImage);


				JourneyCard temp2 = new JourneyCard();
				temp2.Name = "Prueba3";
				temp2.Image = ImageSource.FromResource(CommonVariables.GenericImage);

				StartJourneys.Add(temp1);
				StartJourneys.Add(temp2);
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

		async void OnJourneySelected(JourneyCard journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync(nameof(JournalModelationView));
		}
	}
}
