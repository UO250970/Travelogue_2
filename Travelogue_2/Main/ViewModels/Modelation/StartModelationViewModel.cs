using System.Collections.ObjectModel;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Services;
using Travelogue_2.Main.Views.Modelation.Modelate;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation
{
	public class StartModelationViewModel : DataBaseViewModel
	{

		public Command<JourneyCard> JourneyTapped { get; }
		public ObservableCollection<JourneyCard> StartJournals { get; set; }

		public StartModelationViewModel()
		{
			StartJournals = new ObservableCollection<JourneyCard>();

			JourneyTapped = new Command<JourneyCard>(OnJourneySelected);

			ExecuteLoadDataCommand();
		}

		public override void LoadData()
		{
			StartJournals.Clear();
			JourneyCard temp1 = new JourneyCard();
			temp1.Name = "Prueba";
			temp1.Image = ImageSource.FromResource(CommonVariables.GenericImage);


			JourneyCard temp2 = new JourneyCard();
			temp2.Name = "Prueba3";
			temp2.Image = ImageSource.FromResource(CommonVariables.GenericImage);

			StartJournals.Add(temp1);
			StartJournals.Add(temp2);
		}

		async void OnJourneySelected(JourneyCard journey)
		{
			if (journey == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync(nameof(JournalModelationView));
		}

	}
}
