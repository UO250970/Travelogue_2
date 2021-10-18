using System;
using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels.Journey;
using Travelogue_2.Main.Views.Journey;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
    public class ClosedJourneysViewModel : DataBaseViewModel
    {

		public Command SearchJourneyCommand { get; }
		public Command<JourneyModel> JourneyTapped { get; }
		public Command<JourneyModel> JourneyTappedDelete { get; }

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

			DataBaseUtil.GetJourneysClosed()?.ForEach(x =>
			{
				JourneysClosed.Add(x);
				JourneysClosedSearched.Add(x);
			});
		}

		public ObservableCollection<JourneyModel> journeysClosed;
		public ObservableCollection<JourneyModel> JourneysClosed
		{
			get => journeysClosed;
			set => SetProperty(ref journeysClosed, value);
		}

		public ObservableCollection<JourneyModel> journeysClosedSearched;
		public ObservableCollection<JourneyModel> JourneysClosedSearched
		{
			get => journeysClosedSearched;
			set => SetProperty(ref journeysClosedSearched, value);
		}

		#region SearchText
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
		#endregion

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
			CurrentJourneyId = journey.Id.ToString();
			await Shell.Current.GoToAsync($"{nameof(JourneyView)}");
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

		public override void OnAppearing()
		{
			LoadData();
		}

	}
}
