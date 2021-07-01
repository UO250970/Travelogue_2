using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class ImageModel
	{
		public int Id { get; set; }
		public ImageSource ImageSour { get; set; } = CommonVariables.GetGenericImageImage();
		public string Journey { get; set; } = App.LocResources["NoJourneyAssociated"];
		public string ImageCaption { get; set; } = string.Empty;
	}
}
