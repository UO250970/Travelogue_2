using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels.Journey;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.Library;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Library
{
    public class LibraryViewModel : DataBaseViewModel
	{
		public Command LoadJourneysCommand { get; }
		public Command<JourneyModel> JourneyTapped { get; }
		public Command CreatedJourneysViewCommand { get; }
        public Command ClosedJourneysViewCommand { get; }


		public ObservableCollection<JourneyModel> journeysCreated;
		public ObservableCollection<JourneyModel> JourneysCreated
		{
			get => journeysCreated;
			set => SetProperty(ref journeysCreated, value);
		}

		public ObservableCollection<JourneyModel> journeysClosed;
		public ObservableCollection<JourneyModel> JourneysClosed
		{
			get => journeysClosed;
			set => SetProperty(ref journeysClosed, value);
		}

		public LibraryViewModel()
		{
			LoadJourneysCommand = new Command(() => ExecuteLoadDataCommand());
			
			CreatedJourneysViewCommand = new Command(() => CreatedJourneysViewC());
			ClosedJourneysViewCommand = new Command(() => ClosedJourneysViewC());

			JourneysCreated = new ObservableCollection<JourneyModel>();
			JourneysClosed = new ObservableCollection<JourneyModel>();

			JourneyTapped = new Command<JourneyModel>(OnJourneySelected);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			JourneysCreated.Clear();
			JourneysClosed.Clear();

			JourneysCreated = new ObservableCollection<JourneyModel>( DataBaseUtil.GetJourneysCreated() );
			JourneysClosed = new ObservableCollection<JourneyModel>( DataBaseUtil.GetJourneysClosed() );
		}

		async internal void CreatedJourneysViewC()
			=> await Shell.Current.GoToAsync(nameof(CreatedJourneysView));
		

		async internal void ClosedJourneysViewC()
			=> await Shell.Current.GoToAsync(nameof(ClosedJourneysView));
		

		async void OnJourneySelected(JourneyModel journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyViewModel.JourneyId)}={journey.Id}");
		}

	}
}
