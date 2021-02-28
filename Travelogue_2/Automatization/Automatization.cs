using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Travelogue_2.Main.Models;
using Travelogue_2.Main.Services;
using Plugin.Permissions;
using System.Threading.Tasks;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;
using System.Diagnostics;
using System.Linq;

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

			Debug.WriteLine("Permision location : " + statusLocation);

			PermissionStatus statusCamera = CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>().Result;
			if (statusCamera != PermissionStatus.Granted)
			{
				statusCamera = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
			}

			Debug.WriteLine("Permision camera : " + statusCamera);

			PermissionStatus statusCalendar = await CrossPermissions.Current.CheckPermissionStatusAsync<CalendarPermission>();
			if (statusCalendar != PermissionStatus.Granted)
			{
				statusCalendar = await CrossPermissions.Current.RequestPermissionAsync<CalendarPermission>();
			}

			Debug.WriteLine("Permision calendar : " + statusCalendar);
		}

		public static void PrepareCountries()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceNameDestiny = "Travelogue_2.Resources.JSON.ISO2CountryChecked.txt";
			var resourceNameEmbassy = "Travelogue_2.Resources.JSON.ESEmbassyList.txt";
			string result = "";

			using (Stream stream = assembly.GetManifestResourceStream(resourceNameDestiny))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}
			var iso2ListDestiny = JsonConvert.DeserializeObject<List<Destiny>>(result);

			result = "";

			using (Stream stream = assembly.GetManifestResourceStream(resourceNameEmbassy))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}
			var iso2ListEmbassy = JsonConvert.DeserializeObject<List<Embassy>>(result);

			foreach (Destiny destiny in iso2ListDestiny)
			{
				destiny.Flag = destiny.Code + "_Flag";
				destiny.Original = true;

				List<Embassy> listEmbassy = iso2ListEmbassy.Where(x => x.Country == destiny.Name)?.ToList();

				destiny.Embassies = listEmbassy;

				CommonVariables.AvailableDestinies.Add(destiny);
			}

			//DataBase.InsertCountries(iso2List);
		}

	}
}
