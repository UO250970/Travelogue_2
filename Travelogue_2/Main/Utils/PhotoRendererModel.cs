using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.ViewModels;
using Xamarin.Forms;

namespace Travelogue_2.Main.Utils
{
	public abstract class PhotoRendererModel : BaseViewModel
	{

		public ImageCard AddImage(MediaFile file)
		{
			ImageCard image = new ImageCard();
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

	}
}
