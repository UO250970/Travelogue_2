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
            PermissionStatus statusCalendar = PermissionStatus.Unknown;
            try
            {
                PermissionStatus statusLocation = CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>().Result;
                if (statusLocation != PermissionStatus.Granted)
                {
                    statusLocation = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();
                }

                Debug.WriteLine("Permision location: " + statusLocation);

                return statusCalendar;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error permision location: " + e.StackTrace);
            }

            return statusCalendar;
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

        public static Position GetPosition()
        {
            Location temp = GetLocationAsync().Result;
            if (temp is null) temp = new Location();
            return new Position(temp.Latitude, temp.Longitude);
        }

    }
}
