using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Utils;
using Plugin.Permissions;
using System.Threading.Tasks;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;
using System.Diagnostics;

namespace Travelogue_2.Automatization
{
	static class Automatization
	{
		public static async Task CheckPermissionsAsync()
		{

			PermissionStatus statusLocation = CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>().Result;
			if (statusLocation != PermissionStatus.Granted)
			{
				statusLocation = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();
			}
			//await Alerter.AlertEnableLocation();
			Debug.WriteLine("Permision location : " + statusLocation);

			PermissionStatus statusCamera = CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>().Result;
			if (statusCamera != PermissionStatus.Granted)
			{
				statusCamera = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
			}

			Debug.WriteLine("Permision camera : " + statusCamera);
		}

		public static void PrepareCountries()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Travelogue_2.Resources.JSON.ISO2Country.txt";
			string result = "";

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}

			var iso2List = JsonConvert.DeserializeObject<List<Destiny>>(result);
			foreach (Destiny country in iso2List)
			{
				country.Flag = country.Code + "_Flag";
				country.Original = true;
				CommonVariables.AvailableDestinies.Add(country);
			}

			//DataBase.InsertCountries(iso2List);
		}

	}
}
