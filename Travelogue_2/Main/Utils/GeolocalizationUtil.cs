using Plugin.Permissions;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace Travelogue_2.Main.Utils
{
	public static class GeolocalizationUtil
	{

        public static async void CheckPermissions()
        {
            PermissionStatus statusLocation = CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>().Result;
            if (statusLocation != PermissionStatus.Granted)
            {
                statusLocation = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();
            } 

            Debug.WriteLine("Permision location : " + statusLocation);
        }

        private static async Task<Location> GetLocationAsync()
        {
            CheckPermissions();
            Location location = null;
            try
            {
                return await Geolocation.GetLastKnownLocationAsync();
            }
            catch (Exception ext)
            {
                Console.WriteLine($"Excepcion: {ext.Message}");
            }
            return location;
        }

        public static Location GetLocation()
        {
            CheckPermissions();
            return GetLocationAsync().Result;
        }

        public static Position GetPosition()
        {
            CheckPermissions();
            Location temp = GetLocationAsync().Result;
            if (temp is null) temp = new Location();
            return new Position(temp.Latitude, temp.Longitude);
        }

    }
}
