using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.ViewModels.Journey;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.Modelation;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
	public class ModelationViewModel : DataBaseViewModel
	{
		public Command LoadJourneysCommand { get; }
		public Command<JourneyModel> JourneyTapped { get; }
		public Command StarJournalViewCommand { get; }
		public Command ContinueJournalViewCommand { get; }
		public Command ClosedJournalViewCommand { get; }


		public ObservableCollection<JourneyModel> JourneysStartEditing { get; }
		public ObservableCollection<JourneyModel> JourneysContinueEditing { get; }
		public ObservableCollection<JourneyModel> JourneysClosedEditing { get; }

		public ModelationViewModel()
		{
			StarJournalViewCommand = new Command(() => StarJournalViewC());
			ContinueJournalViewCommand = new Command(() => ContinueJournalViewC());
			ClosedJournalViewCommand = new Command(() => ClosedJournalViewC());

			JourneysStartEditing = new ObservableCollection<JourneyModel>();
			JourneysContinueEditing = new ObservableCollection<JourneyModel>();
			JourneysClosedEditing = new ObservableCollection<JourneyModel>();

			JourneyTapped = new Command<JourneyModel>(OnJourneySelected);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			JourneysStartEditing.Clear();
			JourneyModel temp1 = new JourneyModel();
			temp1.Name = "Prueba";


			JourneyModel temp2 = new JourneyModel();
			temp2.Name = "Prueba2";

			JourneysStartEditing.Add(temp1);
			JourneysStartEditing.Add(temp2);
		}

		async internal void StarJournalViewC() 
			=> await Shell.Current.GoToAsync(nameof(StartModelationView));

		async internal void ContinueJournalViewC()
			=> await Shell.Current.GoToAsync(nameof(ContinueModelationView));

		async internal void ClosedJournalViewC() 
			=> await Shell.Current.GoToAsync(nameof(EndedModelationView));

		async void OnJourneySelected(JourneyModel journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyTemplateViewModel.JourneyId)}={journey.Id}");
		}
	}
}
