using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.Views.Modelation;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
	public class ModelationViewModel : BaseViewModel
	{
		public Command LoadJourneysCommand { get; }
		public Command<Item> JourneyTapped { get; }
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


			ExecuteLoadJourneysCommand();
		}

		async Task ExecuteLoadJourneysCommand()
		{
			IsBusy = true;

			try
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


		async internal void StarJournalViewC() 
			=> await Shell.Current.GoToAsync(nameof(StartModelationView));

		async internal void ContinueJournalViewC()
			=> await Shell.Current.GoToAsync(nameof(ContinueModelationView));

		async internal void ClosedJournalViewC() 
			=> await Shell.Current.GoToAsync(nameof(EndedModelationView));

	}
}
