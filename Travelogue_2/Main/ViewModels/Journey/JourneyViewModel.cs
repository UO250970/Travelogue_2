using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journey
{
    [QueryProperty(nameof(JourneyId), nameof(JourneyId))]
    public class JourneyViewModel : JourneyTemplateViewModel
    {
		public new string JourneyId
		{
			get => journeyId;
			set
			{
				journeyId = value;
				LoadData();
			}
		}
	}
}
