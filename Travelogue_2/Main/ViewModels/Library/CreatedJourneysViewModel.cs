using System.Collections.ObjectModel;
using System.Linq;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Library.Create;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
	public class CreatedJourneysViewModel : DataBaseViewModel
    {

		public Command CreateJourneyCommand { get; }

		public Command SearchJourneyCommand { get; }
		public Command<JourneyModel> JourneyTapped { get; }

		public CreatedJourneysViewModel()
		{
			CreateJourneyCommand = new Command(() => CreateJourneyC());
			SearchJourneyCommand = new Command(() => SearchJourneyC());

			JourneysCreated = new ObservableCollection<JourneyModel>();
			JourneysCreatedSearched = new ObservableCollection<JourneyModel>();

			JourneyTapped = new Command<JourneyModel>(OnJourneySelected);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			JourneysCreated.Clear();
			JourneysCreatedSearched.Clear();

			DataBaseUtil.GetJourneysCreated()?.ForEach(x =>
			{
				JourneysCreated.Add(x);
				JourneysCreatedSearched.Add(x);
			});
		}

		public ObservableCollection<JourneyModel> journeysCreated;
		public ObservableCollection<JourneyModel> JourneysCreated
		{
			get => journeysCreated;
			set => SetProperty(ref journeysCreated, value);
		}

		public ObservableCollection<JourneyModel> journeysCreatedSearched;
		public ObservableCollection<JourneyModel> JourneysCreatedSearched
		{
			get => journeysCreatedSearched;
			set => SetProperty(ref journeysCreatedSearched, value);
		}

		#region SearchText
		async internal void SearchJourneyC()
		{
			SearchText = string.Empty;
			SearchVisible = !SearchVisible;
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
				var temp = JourneysCreated.Where(x => x.Name.ToUpper().Contains(searchText.ToUpper()) == true);
				if (temp.Count() != JourneysCreatedSearched.Count())
				{
					JourneysCreatedSearched.Clear();
					foreach (var card in temp)
					{
						JourneysCreatedSearched.Add(card);
					}
				}
			}
		}
		#endregion

		async internal void CreateJourneyC()
			=> await Shell.Current.GoToAsync(nameof(CreateJourneyView));

		async void OnJourneySelected(JourneyModel journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			CurrentJourneyId = journey.Id.ToString();
			await Shell.Current.GoToAsync($"{nameof(JourneyView)}");
		}

		public override void OnAppearing()
		{
			LoadData();
		}
	}
}
