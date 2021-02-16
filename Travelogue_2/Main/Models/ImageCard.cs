using Travelogue_2.Main.Utils;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class ImageCard
	{
		public ImageSource ImageSour { get; set; } = ImageSource.FromResource(CommonVariables.GenericImageImage);

		public string ImagePath { get; set; } = string.Empty;
		public string ImageName { get; set; } = string.Empty;
		public string ImageFoot { get; set; } = string.Empty;

	}
}
