using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.ViewModels;
using Travelogue_2.Main.ViewModels.Media;
using Travelogue_2.Main.Views.Media;
using Xamarin.Forms;

namespace Travelogue_2.Main.Services
{
	public abstract class PhotoRendererModel : DataBaseViewModel
	{
		public Command<ImageCard> ViewImageCommand { get; set; }

		public ImageCard ImageSelected { get; set; }

		public EntryImageCard AddImage(MediaFile file)
		{
			EntryImageCard image = new EntryImageCard();
			try
			{
				if (file != null)
				{
					image.ImagePath = file.Path;
					//ImageName = CameraUtil.GenerateName();
					image.ImagePath = image.ImagePath + "/" + image.ImageName;

					// TODO Guardar imagen en nueva ruta
					if (!image.ImagePath.Equals(string.Empty))
					{
						Stream stream = file.GetStream();
						image.ImageSour = ImageSource.FromStream(() => stream);
					}
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


		public EntryImageCard blanckImage = new EntryImageCard();
		public EntryImageCard BlanckImage
		{
			get => blanckImage;
			set
			{
				SetProperty(ref blanckImage, value);
			}
		}

		public async void ViewImageC(ImageCard image)
		{
			ImageSelected = image;
			await Shell.Current.GoToAsync($"{nameof(ImageView)}?{ nameof(ImageViewModel.ImagePath)}={ image.ImagePath}");
		}
	}
}
