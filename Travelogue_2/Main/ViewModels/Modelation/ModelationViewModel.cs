using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Modelation;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
	public class ModelationViewModel : DataBaseViewModel
	{
		public Command LoadJourneysCommand { get; }
		public Command<JournalModel> JournalTapped { get; }
		public Command StarJournalViewCommand { get; }
		public Command ContinueJournalViewCommand { get; }
		public Command ClosedJournalViewCommand { get; }

		private ObservableCollection<JournalModel> journalsStartEditing;
		public ObservableCollection<JournalModel> JournalsStartEditing
		{
			get => journalsStartEditing;
			set => SetProperty(ref journalsStartEditing, value);
		}

		public ObservableCollection<JournalModel> journalsContinueEditing;
		public ObservableCollection<JournalModel> JournalsContinueEditing
		{
			get => journalsContinueEditing;
			set => SetProperty(ref journalsContinueEditing, value);
		}

		public ObservableCollection<JournalModel> journalsClosedEditing;
		public ObservableCollection<JournalModel> JournalsClosedEditing
		{
			get => journalsClosedEditing;
			set => SetProperty(ref journalsClosedEditing, value);
		}

		public ModelationViewModel()
		{
			StarJournalViewCommand = new Command(() => StarJournalViewC());
			ContinueJournalViewCommand = new Command(() => ContinueJournalViewC());
			ClosedJournalViewCommand = new Command(() => ClosedJournalViewC());

			JournalsStartEditing = new ObservableCollection<JournalModel>();
			JournalsContinueEditing = new ObservableCollection<JournalModel>();
			JournalsClosedEditing = new ObservableCollection<JournalModel>();

			JournalTapped = new Command<JournalModel>(OnJournalSelected);
		}

		public override void LoadData()
		{
			JournalsStartEditing.Clear();
			//JournalsContinueEditing.Clear();
			//JournalsClosedEditing.Clear();

			//JournalsStartEditing = new ObservableCollection<JournalModel>(DataBaseUtil.GetJournalsToStart());
			DataBaseUtil.GetJournalsToStart()?.ForEach( x => JournalsStartEditing.Add(x) );
			//DataBaseUtil.GetJournalsToContinue()?.ForEach( x => JournalsContinueEditing.Add(x) );
			//DataBaseUtil.GetJournalsClosed()?.ForEach( x => JournalsClosedEditing.Add(x) );
		}

		async internal void StarJournalViewC() 
			=> await Shell.Current.GoToAsync(nameof(StartModelationView));

		async internal void ContinueJournalViewC()
			=> await Shell.Current.GoToAsync(nameof(ContinueModelationView));

		async internal void ClosedJournalViewC() 
			=> await Shell.Current.GoToAsync(nameof(EndedModelationView));

		async void OnJournalSelected(JournalModel journal)
		{
			if (journal == null)
				return;

			if (journal.JournalState != State.CREATED)
            {

            }

			CurrentJourneyId = journal.Id.ToString();
			await Shell.Current.GoToAsync($"{nameof(JournalModelationView)}");
		}
	}
}
