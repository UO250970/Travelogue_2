using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Modelation.Modelate
{
	[QueryProperty("JourneyId", "JourneyId")]
	[QueryProperty("BackgroundId", "BackgroundId")]
	public class PageModelationViewModel : DataBaseViewModel
	{
		private string backgroundId;
		public string BackgroundId
		{
			get => backgroundId;
			set
			{
				backgroundId = value;
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
			if (BackgroundId != null)
            {
				ImageSelectedSource = DataBaseUtil.GetImageById( int.Parse(BackgroundId) ).ImageSour;
            }

			//ImageSelectedSource = CommonVariables.GetBackground(BackgroundSelected);
		}
	}
}
