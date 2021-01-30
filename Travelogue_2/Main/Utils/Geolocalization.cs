using Plugin.Permissions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace Travelogue_2.Main.Utils
{
	public static class Geolocalization
	{

        public static async void CheckPermissions()
        {
            PermissionStatus statusLocation = CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>().Result;
            if (statusLocation != PermissionStatus.Granted)
            {
                statusLocation = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();
            } 
            //await Alerter.AlertEnableLocation();
            System.Diagnostics.Debug.WriteLine("Permision location : " + statusLocation);
        }

        private static async Task<Location> GetLocationAsync()
        {
            CheckPermissions();
            Location loc = null;
            try
            {
                //var request = new GeolocationRequest(GeolocationAccuracy.Medium, timeout: TimeSpan.FromSeconds(10));
                //CancellationTokenSource cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync();

                if (loc == null)
                {
                    loc = Geolocation.GetLastKnownLocationAsync().Result;
                }
                return Geolocation.GetLocationAsync().Result;
            }
            catch (Exception ext)
            {
                Console.WriteLine($"Excepcion: {ext.Message}");
            }
            return loc;
        }

        public static Location GetLocation()
        {
            return GetLocationAsync().Result;
        }

        public static Position GetPosition()
		{
            Location temp = GetLocationAsync().Result;
            if (temp is null) temp = new Location();
            return new Position(temp.Latitude, temp.Longitude);
        }

    }
}
