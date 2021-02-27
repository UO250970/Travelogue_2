using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;

namespace Travelogue_2.Main.ViewModels.Journal
{
	public class JourneySettingsViewModel : BaseViewModel
	{
		public string JourneyId;

		public ObservableCollection<DestinyCard> JourneyDestinies;

		public JourneySettingsViewModel()
		{

			JourneyDestinies = new ObservableCollection<DestinyCard>();

			ExecuteLoadDataCommand();
		}

		async Task ExecuteLoadDataCommand()
		{
			IsBusy = true;

			try
			{
				JourneyId = "1";
				JourneyTitle = "Prueba titulo";

				DestinyCard temp = new DestinyCard();
				Destiny tempDestiny = CommonVariables.AvailableDestinies.Find(x => x.Name == "Canada");
				temp.Destiny = tempDestiny.Name;
				temp.Currency = tempDestiny.Currency;
				Dictionary<string, string> tempEmbassies = new Dictionary<string, string>();
				foreach (Embassy embassy in tempDestiny.Embassies)
				{
					tempEmbassies.Add(embassy.City, embassy.PhoneNumber);
				}
				temp.Embassies = tempEmbassies;

				JourneyDestinies.Add(temp);
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


		public string journeyTitle;

		public string JourneyTitle
		{
			get => journeyTitle;
			set => SetProperty(ref journeyTitle, value);
		}
	}
}
