using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Travelogue_2.Main.ViewModels;
using Travelogue_2.Main.ViewModels.Media;
using Travelogue_2.Main.Views.Media;
using Xamarin.Forms;

namespace Travelogue_2.Main.Services
{
	public abstract class PhotoRendererModel : DataBaseViewModel
	{
		public Command<ImageModel> ViewImageCommand { get; set; }

		public PhotoRendererModel()
		{
			ViewImageCommand = new Command<ImageModel>(x => ViewImageC(x));
		}

		public ImageModel ImageSelected { get; set; }

		public ImageModel AddImage(MediaFile file, string journeyId)
		{
			try
			{
				if (file != null)
				{
					string name = "";
					if (journeyId != null)
					{
						name = DataBaseUtil.GetNameFromJourney(journeyId);
					} else
					{
						name = App.LocResources[CommonVariables.BlankName];
					}
					ImageModel image = DataBaseUtil.CreateImage(file.Path, "", name);
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


		public EntryImageModel blankImage = new EntryImageModel()
		{
			Path = CommonVariables.Blank
		};
		public EntryImageModel BlankImage
		{
			get => blankImage;
			set
			{
				SetProperty(ref blankImage, value);
			}
		}

		public async void ViewImageC(ImageModel image)
		{
			ImageSelected = image;
			await Shell.Current.GoToAsync($"{nameof(ImageView)}?{ nameof(ImageViewModel.ImageId)}={ image.Id}");
		}
	}
}
