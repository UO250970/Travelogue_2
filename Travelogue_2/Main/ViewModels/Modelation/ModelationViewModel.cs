using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
	public class ModelationViewModel : BaseViewModel
	{
		public Command StarJournalViewCommand { get; }
		public Command ContinueJournalViewCommand { get; }
		public Command ClosedJournalViewCommand { get; }

		public ModelationViewModel()
		{
			StarJournalViewCommand = new Command(() => StarJournalViewC());
			ContinueJournalViewCommand = new Command(() => ContinueJournalViewC());
			ClosedJournalViewCommand = new Command(() => ClosedJournalViewC());
		}

		async internal void StarJournalViewC() { }
			//=> await Shell.Current.GoToAsync(nameof(CreatedJourneysView));
		async internal void ContinueJournalViewC() { }
			//=> await Shell.Current.GoToAsync(nameof(CreatedJourneysView));
		async internal void ClosedJournalViewC() { }
			//=> await Shell.Current.GoToAsync(nameof(CreatedJourneysView));

	}
}
