using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.Models
{
	public class ImageModel
	{
		public int Id { get; set; }
		public string Path { get; set; } = string.Empty;
		public ImageSource ImageSour
		{
			get
			{
				return Path == CommonVariables.Blank ? CommonVariables.GetGenericImageImage() : 
					Path == string.Empty ? CommonVariables.GetGenericImage() : 
					ImageSource.FromFile(Path);
			}
		}
		public string Journey { get; set; } = App.LocResources["NoJourneyAssociated"];
		public string Caption { get; set; } = string.Empty;
		public bool Blank { get; set; } = false;
	}
}
