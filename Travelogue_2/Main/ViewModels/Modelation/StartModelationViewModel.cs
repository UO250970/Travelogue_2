using System.Collections.ObjectModel;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
	public class StartModelationViewModel : DataBaseViewModel
	{

		public Command<JourneyModel> JourneyTapped { get; }
		public ObservableCollection<JourneyModel> StartJournals { get; set; }

		public StartModelationViewModel()
		{
			StartJournals = new ObservableCollection<JourneyModel>();

			JourneyTapped = new Command<JourneyModel>(OnJourneySelected);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			StartJournals.Clear();
			JourneyModel temp1 = new JourneyModel();
			temp1.Name = "Prueba";


			JourneyModel temp2 = new JourneyModel();
			temp2.Name = "Prueba3";

			StartJournals.Add(temp1);
			StartJournals.Add(temp2);
		}

		async void OnJourneySelected(JourneyModel journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync(nameof(JournalModelationView));
		}

	}
}
