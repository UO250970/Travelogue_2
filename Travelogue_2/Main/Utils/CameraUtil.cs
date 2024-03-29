﻿using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace Travelogue_2.Main.Utils
{
    public static class CameraUtil
    {
        public static async Task<PermissionStatus> CheckPermissions()
        {
            PermissionStatus statusCamera = PermissionStatus.Unknown;
            try
            {
                statusCamera = CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>().Result;
                if (statusCamera != PermissionStatus.Granted)
                {
                    statusCamera = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
                }

                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                var directoryname = Path.Combine(documents, "Travelogue_2");
                Directory.CreateDirectory(directoryname);

                Debug.WriteLine("Permision camera: " + statusCamera);

                Start();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error permision camera: " + e.StackTrace);
            }

            return statusCamera;
        }

        private static async void Start()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await CrossMedia.Current.Initialize();
            }
        }

        internal static async Task<ImageModel> Photo(PhotoRendererModel model, bool temporal = false)
        {
            await CheckPermissions();
            string photo = await Alerter.AlertPhoto();
            if (photo != null)
            {
                if (photo.Equals(App.LocResources["TakePhoto"])) 
                {
                    return model.AddImage(await TakePhoto(), temporal);
                }
                else if (photo.Equals(App.LocResources["PickPhoto"])) 
                {
                    return model.AddImage(await PickPhoto(), temporal);
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
                    SaveMetaData = true
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
