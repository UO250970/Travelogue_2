using System.Collections.ObjectModel;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.ViewModels.Journal;
using Travelogue_2.Main.Views.Journey;
using Travelogue_2.Main.Views.Modelation;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
	public class ModelationViewModel : DataBaseViewModel
	{
		public Command LoadJourneysCommand { get; }
		public Command<JourneyCard> JourneyTapped { get; }
		public Command StarJournalViewCommand { get; }
		public Command ContinueJournalViewCommand { get; }
		public Command ClosedJournalViewCommand { get; }


		public ObservableCollection<JourneyCard> JourneysStartEditing { get; }
		public ObservableCollection<JourneyCard> JourneysContinueEditing { get; }
		public ObservableCollection<JourneyCard> JourneysClosedEditing { get; }

		public ModelationViewModel()
		{
			StarJournalViewCommand = new Command(() => StarJournalViewC());
			ContinueJournalViewCommand = new Command(() => ContinueJournalViewC());
			ClosedJournalViewCommand = new Command(() => ClosedJournalViewC());

			JourneysStartEditing = new ObservableCollection<JourneyCard>();
			JourneysContinueEditing = new ObservableCollection<JourneyCard>();
			JourneysClosedEditing = new ObservableCollection<JourneyCard>();

			JourneyTapped = new Command<JourneyCard>(OnJourneySelected);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			JourneysStartEditing.Clear();
			JourneyCard temp1 = new JourneyCard();
			temp1.Name = "Prueba";
			temp1.Image = ImageSource.FromResource(CommonVariables.GenericImage);


			JourneyCard temp2 = new JourneyCard();
			temp2.Name = "Prueba2";
			temp2.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			JourneysStartEditing.Add(temp1);
			JourneysStartEditing.Add(temp2);
		}

		async internal void StarJournalViewC() 
			=> await Shell.Current.GoToAsync(nameof(StartModelationView));

		async internal void ContinueJournalViewC()
			=> await Shell.Current.GoToAsync(nameof(ContinueModelationView));

		async internal void ClosedJournalViewC() 
			=> await Shell.Current.GoToAsync(nameof(EndedModelationView));

		async void OnJourneySelected(JourneyCard journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(JourneyView)}?{nameof(JourneyTemplateViewModel.JourneyId)}={journey.Id}");
		}
	}
}
