using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class ImageModel
	{
		public int Id { get; set; }
		public ImageSource ImageSour { get; set; } = ImageSource.FromResource(CommonVariables.GenericImageImage);

		public string ImagePath { get; set; } = string.Empty;
		public string ImageName { get; set; } = string.Empty;
		public string ImageCaption { get; set; } = string.Empty;
	}
}
