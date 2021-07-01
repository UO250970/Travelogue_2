using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.ViewModels;
using Travelogue_2.Main.ViewModels.Media;
using Travelogue_2.Main.Views.Media;
using Xamarin.Forms;

namespace Travelogue_2.Main.Services
{
	public abstract class PhotoRendererModel : DataBaseViewModel
	{
		public Command<ImageModel> ViewImageCommand { get; set; }

		public ImageModel ImageSelected { get; set; }

		public EntryImageModel AddImage(MediaFile file)
		{
			EntryImageModel image = new EntryImageModel();

			try
			{
				if (file != null)
				{
					//image.ImagePath = file.Path;
					//ImageName = CameraUtil.GenerateName();
					image.ImageCaption = string.Empty;
					image.ImageSour = ImageSource.FromFile( file.Path );

					// TODO Guardar imagen en nueva ruta
					//if (!image.ImagePath.Equals(string.Empty))
					//{
						//Stream stream = file.GetStream();
						//image.ImageSour = ImageSource.FromStream(() => stream);
					//}
					return image;
				}
				return null;

			} catch(Exception ex)
			{
				Debug.WriteLine(ex);
				return null;
			}
			
		}

		public int CoverImageHeight { get => CommonVariables.ImageMaxHeight; }

		public int CardImagesHeight { get => CommonVariables.ImageCardMaxHeight; }


		public EntryImageModel blanckImage = new EntryImageModel();
		public EntryImageModel BlanckImage
		{
			get => blanckImage;
			set
			{
				SetProperty(ref blanckImage, value);
			}
		}

		public async void ViewImageC(ImageModel image)
		{
			ImageSelected = image;
			await Shell.Current.GoToAsync($"{nameof(ImageView)}?{ nameof(ImageViewModel.ImageId)}={ image.Id}");
		}
	}
}
