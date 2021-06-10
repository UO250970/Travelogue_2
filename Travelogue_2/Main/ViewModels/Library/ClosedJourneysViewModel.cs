using System;
using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels.Journal;
using Travelogue_2.Main.Views.Journey;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
    public class ClosedJourneysViewModel : DataBaseViewModel
    {

		public Command SearchJourneyCommand { get; }
		public Command<JourneyModel> JourneyTapped { get; }
		public Command<JourneyModel> JourneyTappedDelete { get; }
		public ObservableCollection<JourneyModel> JourneysClosed { get; set; }
		public ObservableCollection<JourneyModel> JourneysClosedSearched { get; set; }


		public ClosedJourneysViewModel()
		{
			SearchJourneyCommand = new Command(() => SearchJourneyC());

			JourneysClosed = new ObservableCollection<JourneyModel>();
			JourneysClosedSearched = new ObservableCollection<JourneyModel>();

			JourneyTapped = new Command<JourneyModel>(OnJourneySelected);
			JourneyTappedDelete = new Command<JourneyModel>(OnJourneySelectedDelete);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			JourneysClosed.Clear();
			JourneysClosedSearched.Clear();

			JourneysClosed = new ObservableCollection<JourneyModel>( DataBaseUtil.GetJourneysClosed() );
			JourneysClosedSearched = new ObservableCollection<JourneyModel>( JourneysClosed );
		}

		private bool searchVisible = false;

		public bool SearchVisible
		{
			get => searchVisible;
			set => SetProperty(ref searchVisible, value);
		}

		private string searchText = string.Empty;
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
			SearchText = string.Empty;
			SearchVisible = !SearchVisible;
		}

		async void OnJourneySelected(JourneyModel journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyTemplateViewModel.JourneyId)}={journey.Id}");
		}

		async void OnJourneySelectedDelete(JourneyModel journey)
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
