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
using Plugin.Settings.Abstractions;
using Travelogue_2.Main.Utils;
using System;
using Travelogue_2.Main.BBDD;

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
			string result = string.Empty;

			using (Stream stream = assembly.GetManifestResourceStream(resourceNameDestiny))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}
			var iso2ListDestiny = JsonConvert.DeserializeObject<List<Destiny>>(result);

			result = string.Empty;

			using (Stream stream = assembly.GetManifestResourceStream(resourceNameEmbassy))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}
			var iso2ListEmbassy = JsonConvert.DeserializeObject<List<Embassy>>(result);

			foreach (Destiny destiny in iso2ListDestiny)
			{
				destiny.Original = true;

				List<Embassy> listEmbassy = iso2ListEmbassy.Where(x => x.Country == destiny.Name)?.ToList();

				destiny.Embassies = listEmbassy;

				CommonVariables.AvailableDestinies.Add(destiny);
			}

			DataBaseUtil.InsertDestinies(iso2ListDestiny);
		}
		
		private static void CreateFutur()
		{
			JourneyModel journey = DataBaseUtil.CreateJourney("StandardTrip", DateTime.Today.AddDays(3), DateTime.Today.Date.AddDays(5));
			//Entry entry = new Entry(journey.Days[0], "Standard", "Description");
			//Text_Info info = new Text_Info(entry, "Texto", DateTime.Now);

			DataBaseUtil.JourneyInsertDestiny(journey, "Australia");

			EntryModel entry = DataBaseUtil.JourneyInsertEntry(journey, 1, "Standard");

			//DataBase.InsertJourney(journey);
			//DataBase.UpdateCountry(country);
			//DataBase.GetJourney(journey);

			//DataBase.GetCountryByName("Australia");

			//Entry entry2 = new Entry(journey.Days[1], "Standard");

			//DataBase.InsertEntry(entry2);

			//DataBase.GetJourney(journey);

			DataBaseUtil.EntryInsertImage(entry, "Path", "Nombre", "foot");

			//ImageModel info2 = new ImageModel(entry, "Path", "Nombre", "foot", DateTime.Now);

			//DataBase.InsertIData(info2);
			//DataBase.GetJourney(journey);

			//Event_Info ivent = new Event_Info(journey.Days[2], "Name", DateTime.Now);

			//DataBase.InsertEvent(ivent);
			//DataBase.GetJourney(journey);
		}

		private static void CreateOnCourse()
		{
			DataBaseUtil.CreateJourney("StandardTrip", DateTime.Today.AddDays(-1), DateTime.Today.Date.AddDays(1));
		}

		private static void CreateFinished()
		{
			DataBaseUtil.CreateJourney("StandardTrip", DateTime.Today.AddDays(-5), DateTime.Today.Date.AddDays(-3));
		}

		private static void ClearDB()
		{
			DataBaseUtil.ClearDataBase();
		}

		public static async void PrepareBd(ISettings properties)
		{
			//ClearDB();

			CheckPermissionsAsync();

			if (DataBaseUtil.HasJourneis() != "0")
			{
				properties.Clear();
			}
			if (DataBaseUtil.HasDestinies() != "0")
			{
				PrepareCountries();
			}

			CreateOnCourse();
			CreateFutur();
			CreateFinished();
		}

	}
}
