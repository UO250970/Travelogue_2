using Travelogue_2.Main.Services;
using Xamarin.Forms;

namespace Travelogue_2.Main.ViewModels.Media
{
	[QueryProperty(nameof(ImagePath), nameof(ImagePath))]
	public class ImageViewModel : PhotoRendererModel
	{
		private string imagePath;
		public string ImagePath
		{
			get => imagePath;
			set
			{
				SetProperty(ref imagePath, value);
				LoadData();
			}
		}

		private string imageName;

		public string ImageName
		{
			get => imageName;
			set => SetProperty(ref imageName, value);
		}

		private ImageSource imageSource;

		public ImageSource ImageSource 
		{
			get => imageSource;
			set => SetProperty(ref imageSource, value);
		}

		public override void LoadData()
		{
			ImageSource = ImageSource.FromResource(ImagePath);
			ImageName = "Prueba de imagen";
		}
	}
}
