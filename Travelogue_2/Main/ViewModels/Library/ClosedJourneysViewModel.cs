using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.ViewModels.Journal;
using Travelogue_2.Main.Views.Journey;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
    public class ClosedJourneysViewModel : BaseViewModel
    {

		public Command SearchJourneyCommand { get; }
		public Command<JourneyCard> JourneyTapped { get; }
		public Command<JourneyCard> JourneyTappedDelete { get; }
		public ObservableCollection<JourneyCard> JourneysClosed { get; set; }
		public ObservableCollection<JourneyCard> JourneysClosedSearched { get; set; }


		public ClosedJourneysViewModel()
		{
			SearchJourneyCommand = new Command(() => SearchJourneyC());

			JourneysClosed = new ObservableCollection<JourneyCard>();
			JourneysClosedSearched = new ObservableCollection<JourneyCard>();

			JourneyTapped = new Command<JourneyCard>(OnJourneySelected);
			JourneyTappedDelete = new Command<JourneyCard>(OnJourneySelectedDelete);

			ExecuteLoadJourneysCommand();
		}

		async Task ExecuteLoadJourneysCommand()
		{
			IsBusy = true;

			try
			{
				/*JourneysClosed.Clear();
				JourneyCard temp1 = new JourneyCard();
				temp1.Name = "Prueba";
				temp1.Image = ImageSource.FromResource(CommonVariables.GenericImage);


				JourneyCard temp2 = new JourneyCard();
				temp2.Name = "Prueba3";
				temp2.Image = ImageSource.FromResource(CommonVariables.GenericImage);

				JourneysClosed.Add(temp1);
				JourneysClosed.Add(temp2);*/
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

		private bool searchVisible = false;

		public bool SearchVisible
		{
			get => searchVisible;
			set => SetProperty(ref searchVisible, value);
		}

		private string searchText = "";
		public string SearchText
		{
			get => searchText;
			set
			{
				SetProperty(ref searchText, value);
				var temp = JourneysClosed.Where(x => x.Name.ToUpper().Contains(searchText.ToUpper()) == true);
				if (temp.Count() != JourneysClosedSearched.Count())
				{
					JourneysClosedSearched.Clear();
					foreach (var card in temp)
					{
						JourneysClosedSearched.Add(card);
					}
				}
			}
		}

		async internal void SearchJourneyC()
		{
			SearchText = "";
			SearchVisible = !SearchVisible;
		}

		async void OnJourneySelected(JourneyCard journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyOngoingView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

		async void OnJourneySelectedDelete(JourneyCard journey)
		{
			if (journey == null)
				return;

			bool result = await Alerter.AlertDeleteJourney();
			if (result)
			{
				JourneysClosed.Remove(journey);
				JourneysClosedSearched.Remove(journey);
			}
		}

	}
}
