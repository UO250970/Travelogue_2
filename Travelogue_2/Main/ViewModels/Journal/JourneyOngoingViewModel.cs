using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Journal
{
	public class JourneyOngoingViewModel : BaseViewModel
	{

		public ImageSource CoverImage 
		{
			get { return ImageSource.FromResource(CommonVariables.GenericImage); } 
		}


	}
}
