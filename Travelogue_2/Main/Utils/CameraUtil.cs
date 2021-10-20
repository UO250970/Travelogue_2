using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Travelogue_2.Resources.Localization;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace Travelogue_2.Main.Utils
{
	public static class CameraUtil
	{
		public static async Task<PermissionStatus> CheckPermissions()
		{
			PermissionStatus statusCalendar = PermissionStatus.Unknown;
			try
			{
				PermissionStatus statusCamera = CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>().Result;
				if (statusCamera != PermissionStatus.Granted)
				{
					statusCamera = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
				}

				var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
				var directoryname = Path.Combine(documents, "Travelogue");
				Directory.CreateDirectory(directoryname);

				Debug.WriteLine("Permision camera: " + statusCamera);

				Start();

				return statusCalendar;
			}
			catch (Exception e)
			{
				Debug.WriteLine("Error permision camera: " + e.StackTrace);
			}

			return statusCalendar;
		}

		private static async void Start()
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsTakePhotoSupported)
			{
				await CrossMedia.Current.Initialize();
			}
		}

		internal static async Task<ImageModel> Photo(PhotoRendererModel model, string journeyId = null)
		{
			await CheckPermissions();
			string photo = await Alerter.AlertPhoto();
			if (photo != null)
			{
				if (photo.Equals(AppResources.TakePhoto))
				{
					return model.AddImage(await TakePhoto(), journeyId);
				}
				else if (photo.Equals(AppResources.PickPhoto))
				{
					return model.AddImage(await PickPhoto(), journeyId);
				}
			}
			return null;
		}

		private static async Task<MediaFile> TakePhoto()
		{
			try
			{
				//return await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
				return await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
				{
					Directory = CommonVariables.AppName,
					Name = GetPhotoName() + CommonVariables.SavedImagesExtension,
					SaveToAlbum = true,
					SaveMetaData = true,
				});
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Take photo : " + ex);
				return null;
			}
		}

		private static async Task<MediaFile> PickPhoto()
		{
			try
			{
				return await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
				{
					SaveMetaData = true,
				});
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Pick photo : " + ex);
				return null;
			}
		}

		private static string GetPhotoName()
		{
			DateTime today = DateTime.Today;
			return CommonVariables.AppName + "_" + today.Year + today.Month + today.Day;
		}

		public static MediaFile PhotoOngoingJourney()
        {
			//Database y todo eso
			return TakePhoto().Result;
		}

	}
}
