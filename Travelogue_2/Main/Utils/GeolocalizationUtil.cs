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

        public static async Task<PermissionStatus> CheckPermissions()
        {
            PermissionStatus statusLocation = PermissionStatus.Unknown;
            try
            {
                statusLocation = CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>().Result;
                if (statusLocation != PermissionStatus.Granted)
                {
                    statusLocation = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();
                }

                Debug.WriteLine("Permision location: " + statusLocation);

                return statusLocation;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error permision location: " + e.StackTrace);
            }

            return statusLocation;
        }

        private static async Task<Location> GetLocationAsync()
        {
            await CheckPermissions();
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

        public async static Task<Position> GetPosition()
        {
            Location temp = await GetLocationAsync();
            if (temp is null) temp = new Location();
            return new Position(temp.Latitude, temp.Longitude);
        }

    }
}
