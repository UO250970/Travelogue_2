using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Travelogue_2.Main.Models.Cards;

namespace Travelogue_2.Main.ViewModels.Journal
{
	public class JourneySettingsViewModel : BaseViewModel
	{
		public string JourneyId;

		public Command MoreInfoCommand { get; }
		public Command PhoneNumberTappedCommand { get; }
		public ObservableCollection<DestinyCard> JourneyDestinies { get; }

		public JourneySettingsViewModel()
		{
			JourneyDestinies = new ObservableCollection<DestinyCard>();

			MoreInfoCommand = new Command<string>((x) => MoreInfoC(x));
			PhoneNumberTappedCommand = new Command<string>((x) => PhoneNumberTappedC(x));

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
				temp.Code = tempDestiny.Code;
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

		#region JourneyTitle
		public string journeyTitle;

		public string JourneyTitle
		{
			get => journeyTitle;
			set => SetProperty(ref journeyTitle, value);
		}
		#endregion

		async internal void MoreInfoC(string path)
		{
			await Browser.OpenAsync(path);
		}

		internal void PhoneNumberTappedC(string number)
		{
			PhoneDialer.Open(number);

			var temp = DependencyService.Get<ILocalNotifications>();


			temp.SendLocalNotification(
					"Notification title",
					"Notification content / description",
					0
			);
		}
	}
}
