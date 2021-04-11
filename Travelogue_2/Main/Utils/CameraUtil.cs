using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Models.Cards;
using Travelogue_2.Main.Services;
using Travelogue_2.Resources.Localization;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace Travelogue_2.Main.Utils
{
	public static class CameraUtil
	{

		public static async void CheckPermissions()
		{
			PermissionStatus statusCamera = CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>().Result;
			if (statusCamera != PermissionStatus.Granted)
			{
				statusCamera = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
			}

			Debug.WriteLine("Permision camera : " + statusCamera);

			Start();
		}

		private static async void Start()
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsTakePhotoSupported)
			{
				await CrossMedia.Current.Initialize();
			}
		}

		internal static async Task<EntryImageCard> Photo(PhotoRendererModel model)
		{
			CheckPermissions();
			string photo = await Alerter.AlertPhoto();
			if (photo != null)
			{
				if (photo.Equals(AppResources.TakePhoto))
				{
					return model.AddImage(await TakePhoto());
				}
				else if (photo.Equals(AppResources.PickPhoto))
				{
					return model.AddImage(await PickPhoto());
				}
			}
			return null;
		}

		private static async Task<MediaFile> TakePhoto()
		{
			try
			{
				return await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
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
				return await CrossMedia.Current.PickPhotoAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Pick photo : " + ex);
				return null;
			}
		}

	}
}
