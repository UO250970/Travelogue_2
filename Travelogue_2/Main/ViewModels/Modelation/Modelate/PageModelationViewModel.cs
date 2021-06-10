using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation.Modelate
{
	[QueryProperty("JourneyId", "JourneyId")]
	[QueryProperty("BackgroundSelected", "BackgroundSelected")]
	public class PageModelationViewModel : DataBaseViewModel
	{
		private string backgroundSelected;
		public string BackgroundSelected
		{
			get => backgroundSelected;
			set
			{
				backgroundSelected = value;
				LoadData();
			}
		}

		public int journeyId;
		public int JourneyId
		{
			get => journeyId;
			set => SetProperty(ref journeyId, value);
		}

		public ImageSource imageSelectedSource;
		public ImageSource ImageSelectedSource
		{
			get => imageSelectedSource;
			set => SetProperty(ref imageSelectedSource, value);
		}

		public override void LoadData()
		{
			ImageSelectedSource = CommonVariables.GetBackground(BackgroundSelected);
		}
	}
}
