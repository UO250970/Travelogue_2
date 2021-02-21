using Travelogue_2.Main.Views.PopUps;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.PopUps
{
	public class AddToJourneyPopUpModel : BaseViewModel
	{
		public Command AddEventCommand { get; }
		public Command AddEntryCommand { get; }

		public AddToJourneyPopUpModel()
		{
			AddEventCommand = new Command(() => AddEventC());
			AddEntryCommand = new Command(() => AddEntryC());
		}

		async internal void AddEventC()
		{
			var popup = new AddToJourneyEventPopUp();

			//popup.model.journey = JourneyCard;
		}


		async internal void AddEntryC()
		{
			var popup = new AddToJourneyEntryPopUp();

			//popup.model.journey = JourneyCard;
		}

	}
}
